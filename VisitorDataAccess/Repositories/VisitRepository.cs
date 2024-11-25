using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;

namespace VisitorDataAccess.Repositories
{
    public class VisitRepository : GenericRepository<Visit>, IVisitRepository
    {
        private readonly VisitorDbContext _dbContext;

        public VisitRepository(VisitorDbContext context) : base(context)
        {
            _dbContext = context;
        }

        // Get active visit for a specific visitor (if EndTime is null)
        public async Task<Visit?> GetActiveVisitByVisitorAsync(long visitorId)
        {
            return await _dbContext.Visits
                .FirstOrDefaultAsync(v => v.Visitor.Id == visitorId && v.EndTime == null);
        }

        // Update an existing visit
        public async Task UpdateVisitAsync(Visit visit)
        {
            _dbContext.Visits.Update(visit);
            await _dbContext.SaveChangesAsync();
        }
    }
}
