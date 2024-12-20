using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Api.Data
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
    {
        private readonly TaskDbContext _context;
        private readonly ILogger<EntityRepository<TEntity>> _logger;
        private readonly DbSet<TEntity> _dbSet;

        public EntityRepository(TaskDbContext context, ILogger<EntityRepository<TEntity>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all entities of type {EntityType}.", typeof(TEntity).Name);
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all entities of type {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching entity of type {EntityType} with ID {EntityId}.", typeof(TEntity).Name, id);
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching entity of type {EntityType} with ID {EntityId}.", typeof(TEntity).Name, id);
                throw;
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                _logger.LogWarning("Attempted to add a null entity of type {EntityType}.", typeof(TEntity).Name);
                throw new ArgumentNullException(nameof(entity), "The entity to add cannot be null.");
            }

            try
            {
                _logger.LogInformation("Adding a new entity of type {EntityType}.", typeof(TEntity).Name);
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new entity of type {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                _logger.LogWarning("Attempted to update a null entity of type {EntityType}.", typeof(TEntity).Name);
                throw new ArgumentNullException(nameof(entity), "The entity to update cannot be null.");
            }

            try
            {
                _logger.LogInformation("Updating an entity of type {EntityType}.", typeof(TEntity).Name);
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating an entity of type {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting entity of type {EntityType} with ID {EntityId}.", typeof(TEntity).Name, id);
                var entity = await GetByIdAsync(id);

                if (entity == null)
                {
                    _logger.LogWarning("Entity of type {EntityType} with ID {EntityId} was not found.", typeof(TEntity).Name, id);
                    return false;
                }

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting entity of type {EntityType} with ID {EntityId}.", typeof(TEntity).Name, id);
                throw;
            }
        }
    }
}
