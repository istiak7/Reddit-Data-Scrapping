using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reddit_Management_System.Application.Common.Utilities;
using Reddit_Management_System.Application.Dtos.Requests.Users;
using Reddit_Management_System.Application.Features.Subscribers.Commands;
using Reddit_Management_System.Application.Features.Subscribers.Commands.Dtos;
using Reddit_Management_System.Application.Services.Users;
using Reddit_Management_System.Service.Validators;

namespace Reddit_Management_System.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(
            IUserService _userService,
            IMediator _mediator
        ) : ControllerBase
    {
        #region RefreshToken

        [AllowAnonymous]
        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            [FromQuery] int userId,
            [FromQuery] string refreshToken)
        {
            var response = await _userService.LoginWithRefreshTokenAsync(userId, refreshToken);
            return Ok(response);
        }

        #endregion
        
        #region LOGIN

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            [FromServices] IValidator<LoginRequest> loginValidator)
        {
            var errorResult = await request.ValidateModel(loginValidator);
            if (!errorResult.Success)
                return BadRequest(errorResult.Errors);

            var response = await _userService.Login(request);
            return Ok(response);
        }

        #endregion

        #region REGISTRATION

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(
            [FromBody] UserRequest request,
            [FromServices] IValidator<UserRequest> registrationValidator)
        {
            var result = await request.ValidateModel(registrationValidator);
            if (!result.Success)
                return BadRequest(result.Errors);

            var response = await _userService.Add(request);
            return Ok(response);
        }

        #endregion
        
        #region Subscribe
        [AllowAnonymous]
        [HttpPost("Subscribe")]
        public async Task<IActionResult> SaveSubscriberEmail([FromForm] SubscriberCreateDto model, CancellationToken cancellationToken)
        {
            Result result;
            var validationResult = new SubscriberCreateDtoValidator().Validate(model);
            if (!validationResult.IsValid)
            {
                result = Utility.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
            }
            else
            {
                var Command = new SubscriberCreateCommand(model);
                result = await _mediator.Send(Command, cancellationToken);
            }
            return StatusCode(result.StatusCode, result);
        }
        
        #endregion
        
        #region Unsubscribe
        [AllowAnonymous]
        [HttpPost("Unsubscribe")]
        public async Task<IActionResult> Unsubscribe([FromForm] UnsubscriberCreateDto model, CancellationToken cancellationToken)
        {
            Result result;
            var validationResult = new UnsubscriberCreateDtoValidator().Validate(model);
            if (!validationResult.IsValid)
            {
                result = Utility.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
            }
            else
            {
                var Command = new SubscriberDeleteCommand(model);
                result = await _mediator.Send(Command, cancellationToken);
            }
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        

    }
}
