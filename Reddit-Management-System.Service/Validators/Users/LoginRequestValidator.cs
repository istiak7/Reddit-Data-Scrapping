using FluentValidation;
using Reddit_Management_System.Application.Dtos.Requests.Users;

namespace Reddit_Management_System.Service.Validators.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Identifier)
                .NotEmpty().WithMessage("Email or Username is required.")
                .MaximumLength(128).WithMessage("Identifier must not exceed 128 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(128).WithMessage("Password must not exceed 128 characters.");
        }
    }
}
