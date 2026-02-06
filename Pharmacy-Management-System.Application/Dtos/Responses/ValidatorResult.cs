namespace Pharmacy_Management_System.Application.Dtos.Responses
{
    public class ValidatorResult
    {
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }

        public static ValidatorResult Ok() => new() { Success = true };
        public static ValidatorResult Fail(params string[] errors) => new()
        {
            Success = false,
            Errors = errors.ToList()
        };
    }
}
