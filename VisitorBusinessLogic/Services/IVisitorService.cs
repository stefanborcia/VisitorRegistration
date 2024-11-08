using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public interface IVisitorService
    {
        Task RegisterVisitorAsync(SignInVisitorDTO visitorDto);
    }
}
