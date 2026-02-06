namespace Pharmacy_Management_System.Application.Dtos.Responses.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
