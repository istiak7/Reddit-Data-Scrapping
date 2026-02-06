using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.RepositoryInterfaces.Common
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //Task<IList<TEntity>> GetAllAsync();
        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        //Task<IList<TEntity>> GetAllNoTrackingAsync();
        Task<IList<TEntity>> GetAllUntrackedAsync(CancellationToken cancellationToken = default);
        //Task<TEntity?> GetByIdAsync(object id);
        Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        //Task<TEntity?> GetByIdNoTrackingAsync(object id);
        Task<TEntity?> GetByIdUntrackedAsync(object id, CancellationToken cancellationToken = default);
        //Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default);
        //Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        //Task<IList<TEntity>> FindAllNoTrackingAsync(Expression<Func<TEntity, bool>> match);
        Task<IList<TEntity>> FindAllUntrackedAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default);
        //Task<IList<TEntity>> FindAllNoTrackingAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IList<TEntity>> FindAllUntrackedAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        //Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default);
        //Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        //Task<TEntity?> FindNoTrackingAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity?> FindUntrackedAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default);
        //Task<TEntity?> FindNoTrackingAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity?> FindUntrackedAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        //Task<int> CountAsync();
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        //Task<object> InsertAsync(TEntity entity, bool saveChanges = false);
        Task<object> InsertAsync(TEntity entity, bool saveChanges = false, CancellationToken cancellationToken = default);
        //Task InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false);
        Task InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false, CancellationToken cancellationToken = default);
        //Task UpdateAsync(TEntity entity, bool saveChanges = false);
        Task UpdateAsync(TEntity entity, bool saveChanges = false, CancellationToken cancellationToken = default);
        //Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false, CancellationToken cancellationToken = default);
        //Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);


    }
}
