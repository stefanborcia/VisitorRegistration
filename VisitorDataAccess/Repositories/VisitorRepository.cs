using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;
using VisitorDTOs.VisitorDTO;
using Action = VisitorDataAccess.Entities.Action;

namespace VisitorDataAccess.Repositories
{
    public class VisitorRepository : GenericRepository<Visitor>, IVisitorRepository
    {
        private readonly VisitorDbContext _dbContext;

        public VisitorRepository(VisitorDbContext context) : base(context)
        {
            _dbContext = context;
        }

        // Get visitor by email
        public async Task<Visitor?> GetVisitorByEmailAsync(string email)
        {
            return await _dbContext.Visitors.FirstOrDefaultAsync(v => v.Email == email);
        }

        // Get active visit for a specific visitor
        public async Task<Visit?> GetActiveVisitByVisitorAsync(long visitorId)
        {
            return await _dbContext.Visits
                .FirstOrDefaultAsync(v => v.Visitor.Id == visitorId && v.EndTime == null);
        }

        // Get visitor monitoring information
        public async Task<IEnumerable<VisitorMonitoringDTO>> GetVisitorMonitoringAsync()
        {
            return await _dbContext.Visits
                .Where(v => v.EndTime == null) //Only visitors who are signedIn
                .Select(e => new VisitorMonitoringDTO
                {
                    Id = e.Visitor.Id,
                    Name = e.Visitor.Name,
                    Company = e.Visitor.Company,
                    VisitingComapanyName = e.VisitingCompany.Name,
                    AppointmentWith = e.AppointmentWith.Name,
                    StartTime = e.StartTime
                })
                .ToListAsync();
        }

        // Search visitor registrations based on different criteria
        public async Task<IEnumerable<VisitorRegistrationSearchDTO>> GetVisitorRegistrationSearchAsync(string search)
        {
            DateTime? searchDate = null;

            if (DateTime.TryParse(search, out DateTime parsedDate))
            {
                searchDate = parsedDate.Date;
            }

            return await _dbContext.Visits
                .Where(e => (searchDate.HasValue && e.StartTime.Date == searchDate.Value)
                         || e.Visitor.Id.ToString().Contains(search)
                         || e.Visitor.Name.ToLower().Contains(search.ToLower())
                         || e.Visitor.Email.ToLower().Contains(search.ToLower())
                         || e.Visitor.Company.ToLower().Contains(search.ToLower())
                         || e.VisitingCompany.Name.ToLower().Contains(search.ToLower())
                         || e.AppointmentWith.Name.ToLower().Contains(search.ToLower()))
                .Select(e => new VisitorRegistrationSearchDTO
                {
                    Id = e.Visitor.Id,
                    Name = e.Visitor.Name,
                    Email = e.Visitor.Email,
                    Company = e.Visitor.Company,
                    VisitingComapanyName = e.VisitingCompany.Name,
                    AppointmentWithEmployeeName = e.AppointmentWith.Name,
                    TimeSpent = e.EndTime.HasValue
                        ? (e.EndTime.Value - e.StartTime).ToString(@"hh\:mm\:ss")
                        : "In Progress",
                    StartTime = e.StartTime,
                })
                .ToListAsync();
        }

        // Add a visit for a visitor
        public async Task<Visit> AddVisitAsync(Visitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));

            var existingVisitor = await _dbContext.Visitors
                .FirstOrDefaultAsync(v => v.Id == visitor.Id) ?? visitor;

            // Check if the visitor entity is being tracked
            if (_dbContext.Entry(existingVisitor).State == EntityState.Detached)
            {
                _dbContext.Attach(existingVisitor);
            }

            var visit = new Visit
            {
                Id = existingVisitor.Id, 
                StartTime = DateTime.Now,
                CurrentStatus = Action.SignIn
            };

            _dbContext.Visits.Add(visit);

            await _dbContext.SaveChangesAsync();

            return visit;
        }

        // Create a log for a visitor
        public async Task CreateLogAsync(VisitorLog visitorLog)
        {
            if (visitorLog == null)
                throw new ArgumentNullException(nameof(visitorLog));

            // Add the log entry to the database
            _dbContext.VisitorLogs.Add(visitorLog);

            // Save changes asynchronously
            await _dbContext.SaveChangesAsync();
        }

        // Implementing UpdateAsync for Visit entity
        public async Task UpdateAsync(Visit visit)
        {
            _dbContext.Visits.Update(visit);
            await _dbContext.SaveChangesAsync();
        }
    }
}
