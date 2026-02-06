using Microsoft.EntityFrameworkCore;
using Pharmacy_Management_System.Application.RepositoryInterfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Repo.Repositories.Common
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        private DbSet<TEntity> _dbSet;
        protected DbSet<TEntity> DbSet => _dbSet ??= _context.Set<TEntity>();
        //public async Task<int> CountAsync()
        //{
        //    return await DbSet.CountAsync();
        //}
        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.CountAsync(cancellationToken);
        }

        //public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        //{
        //    return await DbSet.Where(match).ToListAsync();
        //}
        public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(match).ToListAsync(cancellationToken);
        }
        //public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    var query = _context.Set<TEntity>().Where(match);
        //    if (includeProperties != null)
        //        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        //    return await query.ToListAsync();
        //}
        public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _context.Set<TEntity>().Where(match);
            if (includeProperties != null)
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync(cancellationToken);
        }
        //public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match)
        //{
        //    return await DbSet.FirstOrDefaultAsync(match);
        //}
        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(match, cancellationToken);
        }

        //public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    var query = _context.Set<TEntity>().Where(match);
        //    if (includeProperties != null)
        //        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        //    return await query.FirstOrDefaultAsync();
        //}
        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _context.Set<TEntity>().Where(match);
            if (includeProperties != null)
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        //public async Task<IList<TEntity>> GetAllAsync()
        //{
        //    return await DbSet.ToListAsync();
        //}
        public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }
        //public async Task<TEntity?> GetByIdAsync(object id)
        //{
        //    return await DbSet.FindAsync(id);
        //}
        public async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync([id], cancellationToken);
        }
        //public async Task<object> InsertAsync(TEntity entity, bool saveChanges = false)
        //{
        //    var rtn = await DbSet.AddAsync(entity);
        //    if (saveChanges)
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    return rtn;
        //}
        public async Task<object> InsertAsync(TEntity entity, bool saveChanges = false, CancellationToken cancellationToken = default)
        {
            var rtn = await DbSet.AddAsync(entity, cancellationToken);
            if (saveChanges)
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            return rtn;
        }

        //public async Task InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false)
        //{
        //    await DbSet.AddRangeAsync(entities);
        //    if (saveChanges)
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //}
        public async Task InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false, CancellationToken cancellationToken = default)
        {
            await DbSet.AddRangeAsync(entities, cancellationToken);
            if (saveChanges)
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        //public async Task UpdateAsync(TEntity entity, bool saveChanges = false)
        //{
        //    //var entry = _context.Entry(entity);
        //    //DbSet.Attach(entity);
        //    //entry.State = EntityState.Modified;
        //    await Task.Run(() =>
        //    {
        //        DbSet.Update(entity);
        //    });
        //    if (saveChanges)
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //}
        public async Task UpdateAsync(TEntity entity, bool saveChanges = false, CancellationToken cancellationToken = default)
        {
            DbSet.Update(entity);
            if (saveChanges)
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        //public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false)
        //{
        //    await Task.Run(() =>
        //    {
        //        DbSet.UpdateRange(entities);
        //    });
        //    if (saveChanges)
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //}
        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false, CancellationToken cancellationToken = default)
        {
            DbSet.UpdateRange(entities);
            if (saveChanges)
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        //public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        //{
        //    return DbSet.AsNoTracking().AnyAsync(filter);
        //}
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            return DbSet.AsNoTracking().AnyAsync(filter, cancellationToken);
        }

        #region No Tracking Methods
        //public async Task<IList<TEntity>> GetAllNoTrackingAsync()
        //{
        //    return await DbSet
        //        .AsNoTracking()
        //        .ToListAsync();
        //}
        public async Task<IList<TEntity>> GetAllUntrackedAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        //public async Task<TEntity?> GetByIdNoTrackingAsync(object id)
        //{
        //    var keyProperty = _context.Model
        //    .FindEntityType(typeof(TEntity))?
        //    .FindPrimaryKey()?
        //    .Properties[0];

        //    if (keyProperty == null)
        //        throw new InvalidOperationException($"Entity {typeof(TEntity).Name} does not have a primary key.");

        //    var parameter = Expression.Parameter(typeof(TEntity), "x");
        //    var property = Expression.Property(parameter, keyProperty.Name);
        //    var equal = Expression.Equal(property, Expression.Constant(id));
        //    var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);

        //    return await DbSet
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(lambda);
        //}
        public async Task<TEntity?> GetByIdUntrackedAsync(object id, CancellationToken cancellationToken = default)
        {
            var keyProperty = _context.Model
                .FindEntityType(typeof(TEntity))?
                .FindPrimaryKey()?
                .Properties[0];

            if (keyProperty == null)
                throw new InvalidOperationException($"Entity {typeof(TEntity).Name} does not have a primary key.");

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, keyProperty.Name);
            var equal = Expression.Equal(property, Expression.Constant(id));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);

            return await DbSet.AsNoTracking().FirstOrDefaultAsync(lambda, cancellationToken);
        }

        //public async Task<IList<TEntity>> FindAllNoTrackingAsync(Expression<Func<TEntity, bool>> match)
        //{
        //    return await DbSet
        //    .Where(match)
        //    .AsNoTracking()
        //    .ToListAsync();
        //}
        public async Task<IList<TEntity>> FindAllUntrackedAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(match)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        //public async Task<IList<TEntity>> FindAllNoTrackingAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    var query = _context.Set<TEntity>().Where(match);
        //    if (includeProperties != null)
        //        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        //    return await query.AsNoTracking().ToListAsync();
        //}
        public async Task<IList<TEntity>> FindAllUntrackedAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _context.Set<TEntity>().Where(match);
            if (includeProperties != null)
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        //public async Task<TEntity?> FindNoTrackingAsync(Expression<Func<TEntity, bool>> match)
        //{
        //    return await DbSet
        //    .AsNoTracking()
        //    .FirstOrDefaultAsync(match);
        //}
        public async Task<TEntity?> FindUntrackedAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(match, cancellationToken);
        }


        //public async Task<TEntity?> FindNoTrackingAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    var query = _context.Set<TEntity>().Where(match);
        //    if (includeProperties != null)
        //        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        //    return await query
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();
        //}
        public async Task<TEntity?> FindUntrackedAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _context.Set<TEntity>().Where(match);
            if (includeProperties != null)
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }
        #endregion
    }
}
