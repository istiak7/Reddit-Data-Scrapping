using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Management_System.Application.Dtos.Requests.Users;
using Pharmacy_Management_System.Domain.Contexts;

namespace Pharmacy_Management_System.Service.Validators.Users
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(3);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x).CustomAsync(async (request, context, cancellationToken) =>
            {
                var existingUser = await dbContext.Users
                    .Where(u => u.Username == request.Username || u.Email == request.Email)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingUser != null)
                {
                    if (existingUser.Username == request.Username)
                    {
                        context.AddFailure(nameof(request.Username), "Username already exists.");
                    }

                    if (existingUser.Email == request.Email)
                    {
                        context.AddFailure(nameof(request.Email), "Email already exists.");
                    }
                }
            });
        }
    }
}
