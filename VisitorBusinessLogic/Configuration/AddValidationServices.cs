using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorBusinessLogic.Validation;
using VisitorDTOs;

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
