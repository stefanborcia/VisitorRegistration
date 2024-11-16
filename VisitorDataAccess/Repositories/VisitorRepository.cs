using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using Action = VisitorDataAccess.Entities.Action;

namespace VisitorDataAccess.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorDbContext _dbContext;

        public VisitorRepository(VisitorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddVisitorAsync(Visitor visitor)
        {
            await _dbContext.Set<Visitor>().AddAsync(visitor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateVisitAsync(Visit visit)
        {
            await _dbContext.Set<Visit>().AddAsync(visit);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Visitor> GetVisitorByEmailAsync(string email)
        {
            return await _dbContext.Set<Visitor>().FirstOrDefaultAsync(v => v.Email == email);
        }

        public async Task<Visit> GetActiveVisitByVisitorAsync(long visitorId)
        {
            return await _dbContext.Set<Visit>().FirstOrDefaultAsync(v => v.Visitor.Id == visitorId && v.EndTime == null);
        }
        public async Task UpdateVisitAsync(Visit visit)
        {
            _dbContext.Set<Visit>().Update(visit);
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateVisitorLogAsync(VisitorLog visitorLog)
        {
            _dbContext.VisitorLogs.Add(visitorLog);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Visit>> GetActiveVisitsByEmployeeAsync(long employeeId)
        {
            return await _dbContext.Visits
                .Where(v => v.AppointmentWith.Id == employeeId && v.CurrentStatus == Action.SignIn)
                .ToListAsync();
        }
    }
}
