using Microsoft.EntityFrameworkCore;
using VisitorBusinessLogic.Services;
using VisitorBusinessLogic.Services.Interfaces;
namespace VisitorAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Define a named CORS policy
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            // Register CORS policy with services collection before Build()
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7185")  // Blazor app URL
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            //Registering the VisitorDataAccess to use the connection string 
            builder.Services.AddVisitorDataAccess(builder.Configuration.GetConnectionString("VisitorManagementDb"));

            // Register services
            builder.Services.AddScoped<IVisitorService, VisitorService>();  
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();


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

            // Enable CORS for requests coming from the Blazor app
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
