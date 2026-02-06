using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Reddit_Management_System.Application.AppSettings;
using Reddit_Management_System.Application.Dtos.Requests.Users;
using Reddit_Management_System.Application.Dtos.Responses.Users;
using Reddit_Management_System.Application.Repositories.Users;
using Reddit_Management_System.Application.Services.Users;
using Reddit_Management_System.Domain.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Reddit_Management_System.Service.Services.Users
{
    public class UserService(
            JWTSettings _jwtSettings,
            IUserRepository _userRepository,
            IDistributedCache _distributedCache
        ) : IUserService
    {
        #region PRIVATE

        private string RefreshToken => Guid.NewGuid().ToString();

        private async Task<UserResponse> LoggedInUserResponse(User user)
        {
            var refreshToken = RefreshToken;
            user.RefreshToken = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(1);

            await _userRepository.UpdateAsync(user);
            await _userRepository.CompleteAsync();

            var token = GenerateJwtToken(user);

            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _jwtSettings.Subject),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("username", user.Username),
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        #region Login

        public async Task<UserResponse> LoginWithRefreshTokenAsync(
            int userId,
            string refreshToken)
        {
            
            var user = await _userRepository.GetValidUserByRefreshTokenAsync(userId);

            if (user == null)
            {
                throw new Exception("Invalid or expired refresh token");
            }

            if (!BCrypt.Net.BCrypt.Verify(refreshToken, user.RefreshToken))
            {
                throw new Exception("Invalid refresh token");
            }

            return await LoggedInUserResponse(user);
        }

        public async Task<UserResponse> Login(LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameOrEmailAsync(request.Identifier);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return await LoggedInUserResponse(user);
            }
            else
            {
                throw new Exception("Invalid credentials");
            }
        }

        #endregion

        #region Registration

        public async Task<UserResponse> Add(UserRequest request)
        {
            var verifiedKey = $"verified_email:{request.Email}";
            var isVerified  = await _distributedCache.GetStringAsync(verifiedKey);
            if(isVerified != "true")
            {
                throw new Exception("Email not verified");
            }
            var user = new User()
            {
                RoleId = request.RoleId,
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            };
            await _distributedCache.RemoveAsync(verifiedKey);
            await _userRepository.AddAsync(user);
            await _userRepository.CompleteAsync();

            return await LoggedInUserResponse(user);
        }

        #endregion
    }
}
