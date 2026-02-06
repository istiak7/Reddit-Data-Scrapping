using Reddit_Management_System.Domain.Entities;

namespace Reddit_Management_System.Domain.Interfaces.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        #region GET


        #endregion

        #region POST

        /// <summary>
        /// Adds a new entity asynchronously to the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        #endregion

        #region UPDATE

        /// <summary>
        /// Updates an existing entity asynchronously in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        #endregion

        #region DELETE


        #endregion

        #region CompleteAsync

        /// <summary>
        /// Completes the unit of work asynchronously, saving all changes made in the context to the database.
        /// </summary>
        /// <returns></returns>
        Task<int> CompleteAsync();

        #endregion
    }
}
