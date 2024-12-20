using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagement.Api.Data
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Fetches all entities of type TEntity.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Fetches a single entity of type TEntity by its ID.
        /// </summary>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new entity of type TEntity.
        /// </summary>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity of type TEntity.
        /// </summary>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity of type TEntity by its ID.
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}
