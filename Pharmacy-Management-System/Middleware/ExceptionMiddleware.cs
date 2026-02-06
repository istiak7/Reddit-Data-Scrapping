using Pharmacy_Management_System.Application.CustomExceptions;

namespace Pharmacy_Management_System.Middleware
{
    public class ExceptionMiddleware(
            RequestDelegate _next,
            ILogger<ExceptionMiddleware> _logger,
            IWebHostEnvironment _environment
        )
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode;
            string message;
            dynamic? errorData = null;

            switch (exception)
            {
                case ArgumentNullException:
                case ArgumentException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "Invalid arguments provided";
                    break;
                case ClientCustomException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = exception.Message;
                    errorData = (exception as ClientCustomException)?.GetErrorData();
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    if (_environment.IsDevelopment())
                    {
                        message = exception.InnerException?.ToString() ?? exception.Message;
                    }
                    else
                    {
                        message = "Sorry something went wrong.";
                    }

                    break;
            }
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = statusCode,
                Message = message,
                Errors = errorData
            }.ToString());
        }
    }
}
