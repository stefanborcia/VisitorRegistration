using VisitorDataAccess.Entities;
using VisitorDTOs;
using VisitorDTOs.VisitorDTO;

namespace VisitorDataAccess.Repositories.Interfaces
{
    public interface IVisitorRepository : IGenericRepository<Visitor>
    {
        Task<Visitor?> GetVisitorByEmailAsync(string email);
        Task<Visit?> GetActiveVisitByVisitorAsync(long visitorId);
        Task<IEnumerable<VisitorMonitoringDTO>> GetVisitorMonitoringAsync();
        Task<IEnumerable<VisitorRegistrationSearchDTO>> GetVisitorRegistrationSearchAsync(string search);
        Task<Visit> AddVisitAsync(Visitor visitor);
        Task CreateLogAsync(VisitorLog visitorLog);
        Task UpdateAsync(Visit activeVisit);
    }
}
