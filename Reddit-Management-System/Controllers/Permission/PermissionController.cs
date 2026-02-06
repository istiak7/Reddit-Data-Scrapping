using Microsoft.AspNetCore.Mvc;
using Reddit_Management_System.Application.Common.Utilities;
using Reddit_Management_System.Application.Features.Permissions.Command;
using Reddit_Management_System.Application.Features.Permissions.Command.Dtos;
using Reddit_Management_System.Application.Features.Roles.Commands;
using Reddit_Management_System.Application.Features.Roles.Commands.Dtos;

namespace Reddit_Management_System.Controllers.Permission
{
    public class PermissionController : BaseController
    {
        #region Command 
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PermissionCreateDto model, CancellationToken cancellationToken)
        {

            Result result;
            var validationResult = new PermissionCreateDtoValidator().Validate(model);
            if (!validationResult.IsValid)
            {
                result = Utility.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
            }
            else
            {
                var Command = new PermissionCreateCommand(model);
                result = await Mediator.Send(Command, cancellationToken);
            }
            return StatusCode(result.StatusCode, result);
        }

        #endregion
    }
}
