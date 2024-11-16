using VisitorDataAccess.Entities;

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
    }
}
