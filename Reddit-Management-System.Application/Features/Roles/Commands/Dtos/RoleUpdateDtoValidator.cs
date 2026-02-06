using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.Features.Roles.Commands.Dtos
{
    public sealed class RoleUpdateDtoValidator : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{Role Name is required}");
            RuleFor(x => x.Description).NotEmpty().WithMessage("{Role Description is required}")
                .MinimumLength(5).WithMessage("Role Description Must be length 5");
        }
    }
}
