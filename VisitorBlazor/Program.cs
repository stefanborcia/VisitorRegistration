using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VisitorBusinessLogic.Validation;
using FluentValidation;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDataAccess.Repositories;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorBusinessLogic.Services;
using VisitorDTOs;
using VisitorBusinessLogic.Configuration;
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

            // Add business logic services (includes validation)
            builder.Services.AddBusinessLogicServices();

            await builder.Build().RunAsync();
        }
    }
}
