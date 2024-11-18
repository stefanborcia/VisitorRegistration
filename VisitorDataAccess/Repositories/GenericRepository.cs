using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Repositories.Interfaces;

namespace VisitorDataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly VisitorDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(VisitorDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllRecordsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetRecordsByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddRecordsAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRecordsAsync(long id)
        {
            var entity = await GetRecordsByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateRecordsAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

    }
}
