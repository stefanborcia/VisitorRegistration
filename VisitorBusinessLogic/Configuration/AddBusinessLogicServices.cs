using Microsoft.Extensions.DependencyInjection;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorBusinessLogic.Services;

namespace VisitorBusinessLogic.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            // Register business logic services
            services.AddScoped<IVisitorService, VisitorService>();

            // Register generic services
            services.AddScoped<ICompanyServiceGeneric, CompanyServiceGeneric>();
            services.AddScoped<IEmployeeServiceGeneric, EmployeeServiceGeneric>();

            // Add validation services
            services.AddValidationServices();

            return services;
        }
    }
}
