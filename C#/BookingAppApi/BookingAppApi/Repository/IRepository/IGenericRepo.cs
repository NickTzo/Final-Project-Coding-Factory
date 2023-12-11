using System.Linq.Expressions;

namespace BookingAppApi.Repository.IRepository
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {

        /// <summary>
        /// Retrieves all data from the database.
        /// </summary>
        /// <returns>A list of all retrieved data.</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// This method retrieves data from the database, applying an optional filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns >
        /// Returns a list of data that following the filter parameters.
        /// </returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Retrieves an data  from the database by id.
        /// <param name="Id">The id of the entity to retrieve.</param>
        /// <returns>The retrieved data.</returns>
        Task<TEntity> GetByIdAsync(int Id);

        /// <summary>
        /// Inserts a new data of type TEntity into the database.
        /// </summary>
        /// <param name="entity">The data to be inserted.</param>
        /// <returns>The inserted data.</returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Updates an existing data of type TEntity in the database.
        /// </summary>
        /// <param name="entity">The data to be updated.</param>
        /// <returns>The updated data.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Performs a hard delete of an data of type TEntity from the database based on id.
        /// </summary>
        /// <param name="Id">The id of the data to be deleted.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> HardDeleteAsync(int Id);
    }
}

