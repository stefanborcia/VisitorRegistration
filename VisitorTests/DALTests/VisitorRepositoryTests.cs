using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories;
using VisitorDataAccess;

namespace VisitorTests.DALTests
{
    public class VisitorRepositoryTests
    {
        private readonly VisitorDbContext _dbContext;
        private readonly VisitorRepository _visitorRepository;

        public VisitorRepositoryTests()
        {
            var testOptions = new DbContextOptionsBuilder<VisitorDbContext>()
                .UseInMemoryDatabase(databaseName: "VisitorManagementDb")
                .Options;

            _dbContext = new VisitorDbContext(testOptions);
            _visitorRepository = new VisitorRepository(_dbContext);
        }

        [Fact]
        public async Task GetVisitorByEmailAsyncAndReturnVisitorIfVisitorExistsAsync()
        {
            // Test Arrange to add data
            Visitor visitor = new Visitor { Name = "Stefan", Email = "Stefan@TestCompany.com" };
            _dbContext.Visitors.Add(visitor);
            _dbContext.SaveChanges();

            // Act: Actual Execution of method with corrected email query
            var VisitorResult = await _visitorRepository.GetVisitorByEmailAsync("Stefan@TestCompany.com");

            // Assert: Results that are equal or not
            Assert.NotNull(VisitorResult);
            Assert.Equal("Stefan", VisitorResult.Name);
        }

        [Fact]
        public async Task GetActiveVisitByVisitorAsync_ShouldReturnsGetActiveVisitors()
        {
            // Arrange            
            var visitor = new Visitor
            {
                Name = "Stefan",
                Email = "Stefan@TestCompany.com"
            };
            var activeVisit = new Visit
            {
                Visitor = visitor,
                StartTime = DateTime.Now,
                EndTime = null
            };
            _dbContext.Visitors.Add(visitor);
            _dbContext.Visits.Add(activeVisit);
            await _dbContext.SaveChangesAsync();

            // Act
            var VisitorResult = await _visitorRepository.GetActiveVisitByVisitorAsync(visitor.Id);

            // Assert
            Assert.NotNull(VisitorResult);
            Assert.Equal(activeVisit.Id, VisitorResult.Id);
            Assert.Null(VisitorResult.EndTime);
        }

        [Fact]
        public async Task GetVisitorMonitoringAsync_ShouldReturnsVisitorMonitoringAllData()
        {
            // Arrange
            var visitor = new Visitor
            {
                Name = "Stefan",
                Email = "Stefan@TestCompany.com",
                Company = "Noestuin"
            };
            var visitingCompany = new Company { Name = "On Core" };
            var employee = new Employee { Name = "Angelo" };
            var visit = new Visit
            {
                Visitor = visitor,
                VisitingCompany = visitingCompany,
                AppointmentWith = employee,
                StartTime = DateTime.Now
            };

            _dbContext.Visitors.Add(visitor);
            _dbContext.Companies.Add(visitingCompany);
            _dbContext.Employees.Add(employee);
            _dbContext.Visits.Add(visit);
            await _dbContext.SaveChangesAsync();

            // Act
            var VisitorResult = await _visitorRepository.GetVisitorMonitoringAsync();

            // Assert
            Assert.NotNull(VisitorResult);
            var monitoring = VisitorResult.First();
            Assert.Equal(visitor.Name, monitoring.Name);
            Assert.Equal(visitor.Company, monitoring.Company);
            Assert.Equal(visitingCompany.Name, monitoring.VisitingComapanyName);
            Assert.Equal(employee.Name, monitoring.AppointmentWith);
        }
    }
}
