using Reddit_Management_System.Application.Features.Email.Command.Dtos;
using Reddit_Management_System.Application.ServiceInterfaces.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.Features.Email.Command
{
    public sealed record OtpVerificationCommand(VerifyOtpRequestDto model) : Common.MediatR.ICommand;
    public class OtpVerificationCommandHandler : Common.MediatR.ICommandHandler<OtpVerificationCommand>
    {
        private readonly IEmailCommandService _emailCommandService;
        public OtpVerificationCommandHandler(IEmailCommandService emailCommandService)
        {
            _emailCommandService = emailCommandService;
        }
        public async Task<Result> Handle(OtpVerificationCommand command, CancellationToken cancellationToken)
        {
            return await _emailCommandService.OtpVerificationAsync(command.model);

        }
    }
}
