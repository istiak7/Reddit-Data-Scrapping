using Pharmacy_Management_System.Data.DbContexts;
using Pharmacy_Management_System.Domain.Entities;
using Pharmacy_Management_System.Domain.Interfaces.Base;

namespace Pharmacy_Management_System.Repo.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        #region CTOR

        private readonly ApplicationDbContextWrite _dbContextWrite;
        protected readonly ApplicationDbContextWrite _dbContext;

        public BaseRepository(
            ApplicationDbContextWrite dbContext,
            ApplicationDbContextWrite dbContextWrite
            
        )
        {
            _dbContext = dbContext;
            _dbContextWrite = dbContextWrite;
        }

        #endregion

        #region GET


        #endregion

        #region POST

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContextWrite.Set<T>().AddAsync(entity, cancellationToken);
            return entity;
        }

        #endregion

        #region UPDATE

        public async Task UpdateAsync(T entity)
        {
            _dbContextWrite.Set<T>().Update(entity);
            await Task.CompletedTask;
        }

        #endregion

        #region DELETE


        #endregion

        #region CompleteAsync

        public async Task<int> CompleteAsync() => await _dbContextWrite.SaveChangesAsync();

        #endregion

    }
}
