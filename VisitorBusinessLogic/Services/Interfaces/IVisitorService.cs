using VisitorDataAccess.Entities;
using VisitorDTOs;
using VisitorDTOs.VisitorDTO;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface IVisitorService
    {
        Task<Visit> RegisterVisitorAsync(SignInVisitorDTO visitorDto);
        Task SignOutVisitorAsync(SignOutVisitorDTO visitorDto);
        Task<IEnumerable<VisitorMonitoringDTO>> GetVisitorMonitoringAsync();
        Task<IEnumerable<VisitorRegistrationSearchDTO>> GetVisitorRegistrationSearchAsync(string search);
    }
}
