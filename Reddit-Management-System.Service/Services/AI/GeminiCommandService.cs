using Microsoft.Extensions.Configuration;
using Reddit_Management_System.Application.Dtos.Responses.AI;
using Reddit_Management_System.Application.ServiceInterfaces.AI;
using System.Net.Http.Json;
using System.Text.Json;

namespace Reddit_Management_System.Service.Services.AI;

public class GeminiCommandService : IGeminiComamndService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    
    public GeminiCommandService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<List<AIResponse>> RearrangeBatchPostsAsync(List<(string Title, string Description, int Upvotes)> posts)
    {
        var rawPrompt = _configuration["GeminiSettings:RedditPrompt"];
        var ApiKey = _configuration["GeminiSettings:ApiKey"];
        var Model = _configuration["GeminiSettings:Model"];
        var baseUrl = _configuration["GeminiSettings:BaseUrl"];
        // AI Response data formatting
        var postsData = string.Join(",\n", posts.Select((p, i) => $@"
        {{
            ""index"": {i},
            ""title"": ""{p.Title.Replace("\"", "\\\"")}"",
            ""content"": ""{p.Description.Replace("\"", "\\\"")}"",
            ""upvotes"": {p.Upvotes}
        }}"));
        
        var prompt = string.Format(rawPrompt, posts.Count, postsData);
        var requestBody = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };

        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{ baseUrl}/{Model}:generateContent?key={ApiKey}", requestBody);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GeminiResponse>();
                if (result?.Candidates != null && result.Candidates.Count > 0)
                {
                    string aiResponseText = result.Candidates[0].Content.Parts[0].Text.Trim();
                    if (aiResponseText.Contains("```"))
                        aiResponseText = aiResponseText.Replace("```json", "").Replace("```", "").Trim();

                    return JsonSerializer.Deserialize<List<AIResponse>>(aiResponseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Gemini API Error: {response.StatusCode} - {error}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing Gemini batch response: {ex.Message}");
        }
        return null;
    }
}
