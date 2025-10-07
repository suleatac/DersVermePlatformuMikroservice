using FluentValidation;
using FluentValidation.AspNetCore;
using Microservice.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Principal;

namespace Microservice.Shared.Extentions
{
    public static class CommonServiceExt
    {
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
        {
            services.AddHttpContextAccessor();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);
            services.AddScoped<IIdentityService,IdentityService>();
            services.AddAutoMapper(assembly);

            return services;
        }
    }
}
