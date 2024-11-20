using Microsoft.EntityFrameworkCore;
using VisitorBusinessLogic.Configuration;
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
            var connectionString = builder.Configuration.GetConnectionString("VisitorManagementDb");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'VisitorManagementDb' is not configured.");
            }
            builder.Services.AddVisitorDataAccess(connectionString);

            builder.Services.AddBusinessLogicServices();

            // Add controllers
            builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    });


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
