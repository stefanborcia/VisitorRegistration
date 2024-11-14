using VisitorBusinessLogic.Exceptions;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorBusinessLogic.Validation;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly ICompanyRepository _companyRepository;
        public VisitorService(IVisitorRepository visitorRepository,ICompanyRepository companyRepository )
        {
            _visitorRepository = visitorRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Visitor> RegisterVisitorAsync(SignInVisitorDTO visitorDto)
        {
            var validator = new VisitorValidator();
            var validationResult = await validator.ValidateAsync(visitorDto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    throw new Exception(error.ErrorMessage);
                }
            }

            //TODO
            //var existingVisitor = await _visitorRepository.GetVisitorByEmailAsync(visitorDto.Email);

            //if (existingVisitor != null)
            //{
            //    var activeVisit = await _visitorRepository.GetActiveVisitByVisitorAsync(existingVisitor.Id);
            //    if (activeVisit != null)
            //    {
            //        throw new VisitorAlreadySignedInException("Visitor is already signed in.");
            //    }
            //}

            var visitor = new Visitor
            {
                Name = visitorDto.Name,
                Email = visitorDto.Email,
                Company = visitorDto.Company
            };

            await _visitorRepository.AddVisitorAsync(visitor);
            return visitor;
        }

    }
}