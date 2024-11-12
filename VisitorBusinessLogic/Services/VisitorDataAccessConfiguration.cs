using Microsoft.Extensions.DependencyInjection;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using VisitorDataAccess;

namespace VisitorBusinessLogic.Services
{
    public static class VisitorDataAccessConfiguration
    {
        public static IServiceCollection AddVisitorDataAccess(this IServiceCollection services, string connectionString)
        {
            // Register the VisitorDbContext 
            services.AddDbContext<VisitorDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register the repositories
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IVisitorRepository, VisitorRepository>();

            return services;
        }
    }
}
