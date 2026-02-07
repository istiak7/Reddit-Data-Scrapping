using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Reddit_Management_System.Application.Common.Utilities;
using Reddit_Management_System.Application.Features.Email.Command.Dtos;
using Reddit_Management_System.Application.ServiceInterfaces.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reddit_Management_System.Service.Services.Email
{
    public class EmailCommandService : IEmailCommandService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<EmailCommandService> _logger;
        private readonly SmtpSettings _settings;

        public EmailCommandService(IDistributedCache distributedCache, ILogger<EmailCommandService> logger,
            IOptions<SmtpSettings> settings)
        {
            _distributedCache = distributedCache;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task<Result> SendOtpEmailAsync(string email, List<SrapResponseDto> scrap_message)
        {
            //  Send Email
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_settings.From));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = "Hi, today's Idea for you!";
                var sb = GenerateRedditScrapHtml(scrap_message);
                message.Body = new TextPart("html")
                {
                    Text = $@" {sb}" };

                using var client = new SmtpClient();
                // Gmail SMTP Settings
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Use your Email and the NEW APP PASSWORD here
                await client.AuthenticateAsync(_settings.UserName, _settings.Password);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return Utility.GetSuccessMsg("Message sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SMTP Email Error");
                return Utility.GetErrorMsg($"Failed to send email. Error: {ex.Message}");
            }
        }

        #region Html Generation

            private string GenerateRedditScrapHtml(List<SrapResponseDto> dataList)
        {
            if (dataList == null || dataList.Count == 0)
                return
                    "<div style='font-family: sans-serif; padding: 20px; text-align: center; color: #888;'><h3>No data found to generate report.</h3></div>";

            var sb = new StringBuilder();

            // HTML Structure & Styles
            sb.Append("<!DOCTYPE html>");
            sb.Append("<html lang='en'>");
            sb.Append("<head>");
            sb.Append("<meta charset='UTF-8'>");
            sb.Append("<style>");
            sb.Append(
                "body { font-family: 'Segoe UI', Tahoma, sans-serif; background-color: #DAE0E6; margin: 0; padding: 20px; }");
            sb.Append(
                ".wrapper { max-width: 900px; margin: 0 auto; background: #ffffff; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); overflow: hidden; }");
            sb.Append(".header { background-color: #FF4500; color: white; padding: 20px; text-align: center; }");
            sb.Append("table { width: 100%; border-collapse: collapse; table-layout: fixed; }");
            sb.Append(
                "th { background-color: #f8f9fa; color: #1c1c1c; text-align: left; padding: 15px; border-bottom: 2px solid #edeff1; font-size: 13px; text-transform: uppercase; }");
            sb.Append(
                "td { padding: 15px; border-bottom: 1px solid #edeff1; vertical-align: top; word-wrap: break-word; }");
            sb.Append(".title { color: #0079D3; font-weight: 600; font-size: 16px; text-decoration: none; }");
            sb.Append(".description { color: #1a1a1b; line-height: 1.6; font-size: 14px; margin-top: 8px; }");
            sb.Append(
                ".upvotes-badge { background-color: #f6f7f8; color: #FF4500; font-weight: bold; padding: 5px 10px; border-radius: 20px; display: inline-block; }");
            sb.Append(
                ".img-container { margin-top: 10px; border-radius: 4px; border: 1px solid #eee; overflow: hidden; max-width: 100%; }");
            sb.Append(".post-img { width: 100%; height: auto; display: block; }");
            sb.Append("tr:hover { background-color: #f9fafb; }");
            sb.Append("</style>");
            sb.Append("</head>");
            sb.Append("<body>");

            sb.Append("<div class='wrapper'>");
            sb.Append("<div class='header'><h1>Today's Content</h1></div>");
            sb.Append("<table>");
            sb.Append(
                "<thead><tr><th style='width: 75%;'>Content Details</th><th style='width: 25%; text-align: center;'>Engagement</th></tr></thead>");
            sb.Append("<tbody>");

            foreach (var item in dataList)
            {
                sb.Append("<tr>");

                // Content Cell
                sb.Append("<td>");
                sb.Append($"<div class='title'>{WebUtility.HtmlEncode(item.Title)}</div>");

                // Process Description (Newlines + Images)
                string processedBody = FormatDescription(item.Description);
                sb.Append($"<div class='description'>{processedBody}</div>");
                sb.Append("</td>");

                // Upvotes Cell
                sb.Append("<td style='text-align: center; vertical-align: middle;'>");
                sb.Append($"<div class='upvotes-badge'>↑ {item.Upvotes}</div>");
                sb.Append("</td>");

                sb.Append("</tr>");
            }

            sb.Append("</tbody></table>");
            sb.Append("</div>");
            sb.Append("</body></html>");

            return sb.ToString();
        }

        private string FormatDescription(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            // 1. Basic HTML Encoding for security
            string encoded = WebUtility.HtmlEncode(text);

            // 2. Handle New Lines (\n to <br/>)
            string withLines = encoded.Replace("\n", "<br />");

            // 3. Regex to detect Image URLs (png, jpg, jpeg, gif, webp)
            string pattern = @"(https?://\S+\.(?:png|jpg|jpeg|gif|webp))";

            return Regex.Replace(withLines, pattern, match =>
            {
                string url = match.Value;
                return
                    $"<div class='img-container'><img src='{url}' class='post-img' alt='Reddit Media' onerror=\"this.parentElement.style.display='none';\" /></div>";
            });
        }

        #endregion
    
    }
}