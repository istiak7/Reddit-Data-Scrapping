using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.Features.Email.Command.Dtos
{
    public sealed class VerifyOtpRequestValidatorDto : AbstractValidator<VerifyOtpRequestDto>
    {
        public VerifyOtpRequestValidatorDto() {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Otp)
                .NotEmpty().WithMessage("OTP is required.")
                .Length(6).WithMessage("OTP must be 6 characters long.");
        }
    }
}
