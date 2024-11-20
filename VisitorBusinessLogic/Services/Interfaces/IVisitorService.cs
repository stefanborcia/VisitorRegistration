using VisitorDataAccess.Entities;
using VisitorDTOs.VisitorDTO;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface IVisitorService
    {
        Task<Visit> RegisterVisitorAsync(SignInVisitorDTO visitorDto);
        Task SignOutVisitorAsync(SignOutVisitorDTO visitorDto);
    }
}
