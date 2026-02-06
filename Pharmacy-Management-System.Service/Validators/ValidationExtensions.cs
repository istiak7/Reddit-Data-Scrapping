using FluentValidation;
using Pharmacy_Management_System.Application.Dtos.Responses;

namespace Pharmacy_Management_System.Service.Validators
{
    public static class ValidationExtensions
    {
        public static async Task<ValidatorResult> ValidateModel<T>(
            this T model, // this is Method Chaining concept
            IValidator<T> validator,
            CancellationToken cancellationToken = default)
        {
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToArray();
                return ValidatorResult.Fail(errors);
            }

            return ValidatorResult.Ok();
        }
    }
}
