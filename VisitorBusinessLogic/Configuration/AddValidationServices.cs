using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VisitorBusinessLogic.Validation;
using VisitorDTOs.VisitorDTO;

namespace VisitorBusinessLogic.Configuration
{
    public static class ValidationDependencyInjection
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SignInVisitorDTO>, VisitorValidator>();
            services.AddScoped<IValidator<SignOutVisitorDTO>, SignOutVisitorValidator>();

            return services;
        }
    }
}
