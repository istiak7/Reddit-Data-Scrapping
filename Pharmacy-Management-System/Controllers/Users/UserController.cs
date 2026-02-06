using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Management_System.Application.Common.Utilities;
using Pharmacy_Management_System.Application.Dtos.Requests.Users;
using Pharmacy_Management_System.Application.Features.Email.Command;
using Pharmacy_Management_System.Application.Features.Email.Command.Dtos;
using Pharmacy_Management_System.Application.Features.Roles.Commands;
using Pharmacy_Management_System.Application.Features.Roles.Commands.Dtos;
using Pharmacy_Management_System.Application.ServiceInterfaces.Email;
using Pharmacy_Management_System.Application.Services.Users;
using Pharmacy_Management_System.Service.Validators;

namespace Pharmacy_Management_System.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController (
            IUserService _userService,
            IMediator _mediator
        ) : ControllerBase 
    {
        #region Query

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


        #region Command

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

        #endregion


        #region Otp

        [HttpPost("send-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest model, CancellationToken cancellationToken)
        {
            Result result;
            var validationResult = new EmailValidatorDto().Validate(model);
            if(!validationResult.IsValid)
            {
                result = Utility.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
            }
            else
            {
                var Command = new SendEmailCommand(model);
                result = await _mediator.Send(Command, cancellationToken);
            }
            return StatusCode(result.StatusCode, result);

        }
        [HttpPost("verify-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto model,CancellationToken cancellationToken)
        {
            Result result;
            var validationResult = new VerifyOtpRequestValidatorDto().Validate(model);
            if(!validationResult.IsValid)
            {
                result = Utility.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
               
            }
            else
            {
                var Command = new OtpVerificationCommand(model);
                result = await _mediator.Send(Command, cancellationToken);
            }
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
