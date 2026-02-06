using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Application.Features.Permissions.Command.Dtos
{
    public sealed class PermissionCreateDtoValidator : AbstractValidator<PermissionCreateDto>
    {
        public PermissionCreateDtoValidator() 
        {
            RuleFor(x => x.Module).NotEmpty().WithMessage("Module Name is Required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Requied")
                .MinimumLength(3).WithMessage("Name length atleast 3");
        }
    }
}
