using FluentValidation;
using FluentValidation.Results;
using Moq;
using FluentAssertions;
using VisitorBusinessLogic.Services;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs.VisitorDTO;
using Xunit;
using ValidationException = VisitorBusinessLogic.Exceptions.ValidationException;
using Action = VisitorDataAccess.Entities.Action;

namespace VisitorTests
{
    public class VisitorServiceTests
    {
        private readonly Mock<IVisitorRepository> _visitorRepositoryMock;
        private readonly Mock<IVisitRepository> _visitRepositoryMock;
        private readonly Mock<IGenericRepository<Company>> _companyRepositoryMock;
        private readonly Mock<IGenericRepository<Employee>> _employeeRepositoryMock;
        private readonly Mock<IGenericRepository<VisitorLog>> _logRepositoryMock;
        private readonly Mock<IValidator<SignInVisitorDTO>> _signInValidatorMock;
        private readonly Mock<IValidator<SignOutVisitorDTO>> _signOutValidatorMock;

        private readonly VisitorService _visitorService;

        public VisitorServiceTests()
        {
            _visitorRepositoryMock = new Mock<IVisitorRepository>();
            _visitRepositoryMock = new Mock<IVisitRepository>();
            _companyRepositoryMock = new Mock<IGenericRepository<Company>>();
            _employeeRepositoryMock = new Mock<IGenericRepository<Employee>>();
            _logRepositoryMock = new Mock<IGenericRepository<VisitorLog>>();
            _signInValidatorMock = new Mock<IValidator<SignInVisitorDTO>>();
            _signOutValidatorMock = new Mock<IValidator<SignOutVisitorDTO>>();

            _visitorService = new VisitorService(
                _visitorRepositoryMock.Object,
                _visitRepositoryMock.Object,
                _companyRepositoryMock.Object,
                _employeeRepositoryMock.Object,
                _logRepositoryMock.Object,
                _signInValidatorMock.Object,
                _signOutValidatorMock.Object);
        }

        [Fact]
        public async Task RegisterVisitorAsync_ShouldThrowValidationException_WhenVisitorDtoIsInvalid()
        {
            // Arrange
            // Create a DTO (Data Transfer Object) with invalid data
            var visitorDto = new SignInVisitorDTO
            {
                Name = "Test Visitor",
                Email = ""
            };

            // Expected validation error
            var validationErrors = new List<ValidationFailure>
            {
            new ValidationFailure("Email", "Email is required.")
            };

            var validationResult = new ValidationResult(validationErrors);

            _signInValidatorMock.Setup(v => v.ValidateAsync(visitorDto, default))
                .ReturnsAsync(validationResult);

            // Act
            Func<Task> act = async () => await _visitorService.RegisterVisitorAsync(visitorDto);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("Email is required.");
        }

        [Fact]
        public async Task RegisterVisitorAsync_ShouldThrowValidationRegisterVisitor_WhenVisitorDtoIsValid()
        {
            // Arrange
            var visitorDto = new SignInVisitorDTO
            {
                Name = "Stefan",
                Email = "Stefan@TestCompany.com",
                Company = "Noestuin",
                VisitingCompanyId = 1,
                AppointmentWithId = 2
            };

            _signInValidatorMock
                .Setup(v => v.ValidateAsync(visitorDto, default))
                .ReturnsAsync(new ValidationResult());

            _visitorRepositoryMock
                .Setup(v => v.GetVisitorByEmailAsync(visitorDto.Email))
                .ReturnsAsync(null as Visitor);

            _visitorRepositoryMock
                .Setup(v => v.AddAsync(It.IsAny<Visitor>()))
                .Returns(Task.CompletedTask);

            _companyRepositoryMock
                .Setup(c => c.GetByIdAsync(visitorDto.VisitingCompanyId))
                .ReturnsAsync(new Company { Id = 2, Name = "On Core" });

            _employeeRepositoryMock
                .Setup(e => e.GetByIdAsync(visitorDto.AppointmentWithId))
                .ReturnsAsync(new Employee { Id = 2, Name = "Angelo" });

            _visitRepositoryMock
                .Setup(v => v.AddAsync(It.IsAny<Visit>()))
                .Returns(Task.CompletedTask);

            // Act
            var act = await _visitorService.RegisterVisitorAsync(visitorDto);

            // Assert
            act.Should().NotBeNull();
            act.Visitor.Name.Should().Be("Stefan");
            act.CurrentStatus.Should().Be(Action.SignIn);

            _visitorRepositoryMock.Verify(v => v.AddAsync(It.IsAny<Visitor>()), Times.Once);
            _visitRepositoryMock.Verify(v => v.AddAsync(It.IsAny<Visit>()), Times.Once);
        }
        [Fact]
        public async Task SignOutVisitorAsync_ShouldThrowValidationSignOutVisitor_WhenSignOutVisitorDTOValid()
        {
            // Arrange
            var visitorDto = new SignOutVisitorDTO { Email = "Stefan@TestCompany.com" };

            var visitor = new Visitor { Id = 1, Name = "Stefan", Email = "Stefan@TestCompany.com", Company = "Noestuin" };

            var activeVisit = new Visit
            {
                Id = 1,
                Visitor = visitor,
                StartTime = DateTime.Now.AddHours(-3),
                CurrentStatus = Action.SignIn
            };

            _signOutValidatorMock
                .Setup(v => v.ValidateAsync(visitorDto, default))
                .ReturnsAsync(new ValidationResult());

            _visitorRepositoryMock
                .Setup(v => v.GetVisitorByEmailAsync(visitorDto.Email))
                .ReturnsAsync(visitor);

            _visitRepositoryMock
                .Setup(v => v.GetActiveVisitByVisitorAsync(visitor.Id))
                .ReturnsAsync(activeVisit);

            _visitRepositoryMock
                .Setup(v => v.UpdateVisitAsync(It.IsAny<Visit>()))
                .Returns(Task.CompletedTask);

            _logRepositoryMock
                .Setup(vl => vl.AddAsync(It.IsAny<VisitorLog>()))
                .Returns(Task.CompletedTask);

            // Act
            await _visitorService.SignOutVisitorAsync(visitorDto);

            // Assert
            activeVisit.CurrentStatus.Should().Be(Action.SignOut);
            activeVisit.EndTime.Should().NotBeNull();

            _visitRepositoryMock.Verify(v => v.UpdateVisitAsync(activeVisit), Times.Once);
            _logRepositoryMock.Verify(vl => vl.AddAsync(It.IsAny<VisitorLog>()), Times.Once);
        }
    }
}
