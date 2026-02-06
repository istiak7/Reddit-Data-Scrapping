using Reddit_Management_System.Application.Features.Email.Command.Dtos;
using Reddit_Management_System.Application.ServiceInterfaces.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.Features.Email.Command
{
    public sealed record SendEmailCommand(SendOtpRequest model): Common.MediatR.ICommand;
    public class SendEmailCommandHandler : Common.MediatR.ICommandHandler<SendEmailCommand>
    {
        private readonly IEmailCommandService _emailCommandService;
        public SendEmailCommandHandler(IEmailCommandService emailCommandService)
        {
            _emailCommandService = emailCommandService;
        }
        public async Task<Result> Handle(SendEmailCommand command, CancellationToken cancellationToken)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            return await _emailCommandService.SendOtpEmailAsync(command.model.Email, otp);
            
        }
    }
}
