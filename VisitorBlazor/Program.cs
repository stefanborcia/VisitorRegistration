using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VisitorBusinessLogic.Validation;
using FluentValidation;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDataAccess.Repositories;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorBusinessLogic.Services;
namespace VisitorBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Load API base address from configuration
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7162/") });


            builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();

            //Register validator
            builder.Services.AddScoped<IValidator<VisitorDTOs.SignInVisitorDTO>, VisitorValidator>();

            //Register service 
            builder.Services.AddScoped<IVisitorService, VisitorService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            await builder.Build().RunAsync();
        }
    }
}
