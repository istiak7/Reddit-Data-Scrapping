using Reddit_Management_System.Application.Dtos.Requests.Users;
using Reddit_Management_System.Application.Dtos.Responses.Users;

namespace Reddit_Management_System.Application.Services.Users
{
    public interface IUserService
    {
        #region LOGIN

        Task<UserResponse> Login(LoginRequest request);
        Task<UserResponse> LoginWithRefreshTokenAsync(int userId, string refreshToken);

        #endregion

        #region REGISTRATION

        Task<UserResponse> Add(UserRequest request);

        #endregion
    }
}
