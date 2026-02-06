using Microsoft.EntityFrameworkCore;
using Pharmacy_Management_System.Application.Repositories.Users;
using Pharmacy_Management_System.Data.DbContexts;
using Pharmacy_Management_System.Domain.Entities.Users;

namespace Pharmacy_Management_System.Repo.Repositories.Users
{
    public class UserRepository(
            ApplicationDbContextWrite _dbContext
        ) : BaseRepository<User>(_dbContext, _dbContext), IUserRepository
    {
        #region PRIVATE


        #endregion

        #region GET

        public async Task<User?> GetValidUserByRefreshTokenAsync(int userId)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.Id == userId &&
                    x.RefreshToken != null &&
                    x.RefreshTokenExpireTime > DateTime.UtcNow);
        }

        public async Task<User?> GetByUsernameOrEmailAsync(string identifier)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == identifier || x.Email == identifier);
            return user;
        }

        #endregion

        #region POST


        #endregion

        #region PUT


        #endregion

        #region DELETE


        #endregion
    }
}
