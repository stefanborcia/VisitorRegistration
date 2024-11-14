using VisitorDataAccess.Entities;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface IVisitorService
    {
        Task<Visitor> RegisterVisitorAsync(SignInVisitorDTO visitorDto);
    }
}
