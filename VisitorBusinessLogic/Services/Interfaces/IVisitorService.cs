using VisitorDataAccess.Entities;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface IVisitorService
    {
        Task<Visit> RegisterVisitorAsync(SignInVisitorDTO visitorDto);
        Task SignOutVisitorAsync(SignOutVisitorDTO visitorDto);
    }
}
