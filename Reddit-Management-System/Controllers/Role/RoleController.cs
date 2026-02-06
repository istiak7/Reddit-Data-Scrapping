using Microsoft.AspNetCore.Mvc;
using Reddit_Management_System.Application.Common.Utilities;
using Reddit_Management_System.Application.Features.Roles.Commands;
using Reddit_Management_System.Application.Features.Roles.Commands.Dtos;

namespace Reddit_Management_System.Controllers.Role
{
    public class RoleController : BaseController
    {
        #region Command

        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto model, CancellationToken cancellationToken)
        {
            Result result;
            var validationResult = new RoleCreateDtoValidator().Validate(model);
            if (!validationResult.IsValid)
            {
                result = Utility.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
            }
            else
            {
                var Command = new RoleCreateCommand(model);
                result = await Mediator.Send(Command, cancellationToken);
            }
            return StatusCode(result.StatusCode, result);

        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRole([FromBody]  RoleUpdateDto model, CancellationToken cancellationToken)
        {
            Result result;
            var validationResult = new RoleUpdateDtoValidator().Validate(model);
            if (!validationResult.IsValid)
            {
                result = Utility.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
            }
            else
            {
                var Command = new RoleUpdateCommand(model);
                result = await Mediator.Send(Command, cancellationToken);
            }
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
