using VisitorDataAccess.Entities;

namespace VisitorDataAccess.Repositories.Interfaces
{
    public interface IVisitorRepository
    {
        Task AddVisitorAsync(Visitor visitor);          
        Task CreateVisitAsync(Visit visit);           
        Task<Visitor> GetVisitorByEmailAsync(string email); 
        Task<Visit> GetActiveVisitByVisitorAsync(long visitorId);
        Task UpdateVisitAsync(Visit visit);
    }
}
