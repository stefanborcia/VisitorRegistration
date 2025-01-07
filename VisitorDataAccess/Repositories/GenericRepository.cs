using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;

namespace VisitorDataAccess.Repositories
{
    // Generic Repository handling basic CRUD operations for any entity
    public class GenericRepository<T> : IGenericRepository<T> where T : SoftDelete
    {
        private readonly VisitorDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        // Constructor initializes the dbContext and DbSet
        public GenericRepository(VisitorDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        // Get all entities of type T
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
        }

        // Get a single entity by its ID
        public async Task<T> GetByIdAsync(long id)
        {
            //EF.Property - properties not explicitly defined in the entity class but configured in the EF model
            return await _dbSet.Where(e => !e.IsDeleted).FirstOrDefaultAsync(e => EF.Property<long>(e, "Id") == id); 
        }

        // Add a new entity to the context
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Update an existing entity in the context
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete an entity by its ID
        public async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity); 
                await _dbContext.SaveChangesAsync();
            }
        }

        // Soft delete an entity (set IsDeleted to true)
        public async Task SoftDeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true; // Mark as deleted
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
