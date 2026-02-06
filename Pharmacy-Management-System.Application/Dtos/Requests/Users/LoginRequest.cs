namespace Pharmacy_Management_System.Application.Dtos.Requests.Users
{
    public class LoginRequest
    {
        public required string Identifier { get; set; }
        public required string Password { get; set; }
    }
}
