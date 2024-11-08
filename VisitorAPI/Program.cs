using Microsoft.EntityFrameworkCore;
using VisitorBusinessLogic.Services;
using VisitorDataAccess;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDataAccess.Repositories;
namespace VisitorAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            //Register VisitorDbContext
            builder.Services.AddDbContext<VisitorDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("VisitorManagementDb")));

            // Register repositories
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();

            // Register services
            builder.Services.AddScoped<IVisitorService, VisitorService>();  
            builder.Services.AddScoped<ICompanyService, CompanyService>();

            // Add controllers
            builder.Services.AddControllers();

            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enable Swagger middleware only in Development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
