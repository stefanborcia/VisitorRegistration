using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            // Add validation services
            services.AddValidationServices();

            return services;
        }
    }
}
