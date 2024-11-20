using Microsoft.Extensions.DependencyInjection;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using VisitorDataAccess;
using VisitorDataAccess.Entities;

namespace VisitorBusinessLogic.Configuration
{
    public static class VisitorDataAccessConfiguration
    {
        public static IServiceCollection AddVisitorDataAccess(this IServiceCollection services, string connectionString)
        {
            // Register the VisitorDbContext 
            services.AddDbContext<VisitorDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register the repositories
            services.AddScoped<IVisitorRepository, VisitorRepository>();
            services.AddScoped<IGenericRepository<Company>, GenericRepository<Company>>();
            services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();

            // Register Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Add validation services
            services.AddValidationServices();

            return services;
        }
    }
}
