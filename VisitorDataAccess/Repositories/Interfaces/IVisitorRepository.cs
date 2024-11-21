using VisitorDataAccess.Entities;
using VisitorDTOs.VisitorDTO;

namespace VisitorDataAccess.Repositories.Interfaces
{
    public interface IVisitorRepository
    {
        Task AddVisitorAsync(Visitor visitor);          
        Task<Visitor> GetVisitorByEmailAsync(string email); 
        Task<Visit> GetActiveVisitByVisitorAsync(long visitorId);
        Task UpdateVisitAsync(Visit activeVisit);
        Task CreateVisitAsync(Visit newVisit);
        Task CreateVisitorLogAsync(VisitorLog visitorLog);
        Task<IEnumerable<Visit>> GetActiveVisitsByEmployeeAsync(long employeeId);
        Task AddRecordsAsync(Visit visit);
        Task<IEnumerable<VisitorMonitoringDTO>> GetVisitorMonitoringAsync();

    }
}
