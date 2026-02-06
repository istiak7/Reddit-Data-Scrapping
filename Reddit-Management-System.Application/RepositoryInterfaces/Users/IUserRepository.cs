using Reddit_Management_System.Domain.Entities.Users;
using Reddit_Management_System.Domain.Interfaces.Base;

namespace Reddit_Management_System.Application.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        #region GET

        Task<User?> GetValidUserByRefreshTokenAsync(int userId);
        Task<User?> GetByUsernameOrEmailAsync(string identifier);

        #endregion

        #region POST


        #endregion

        #region PUT


        #endregion

        #region DELETE


        #endregion
    }
}
