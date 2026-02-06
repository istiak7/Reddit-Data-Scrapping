using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Reddit_Management_System.Domain.Entities.Users;

namespace Reddit_Management_System.Domain.Contexts
{
    public interface IApplicationDbContext : IInfrastructure<IServiceProvider>
    {
        DatabaseFacade Database { get; }

        #region DbSets

        public DbSet<User> Users { get; set; }

        #endregion

        #region Methods
        void Dispose();

        #endregion
    }
}
