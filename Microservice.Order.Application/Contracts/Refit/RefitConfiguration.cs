using Microservice.Order.Application.Contracts.Refit.PaymentService;
using Microservice.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Application.Contracts.Refit
{
    public static class RefitConfiguration
    {

        public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthenticatedHtttpClientHandler>();

            services.AddScoped<ClientAuthenticatedHttpClientHandler>();


            services.AddOptions<IdentityOption>()
                .BindConfiguration(nameof(IdentityOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<IdentityOption>(sp =>
                sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<IdentityOption>>().Value);




            services.AddOptions<ClientSecretOption>()
              .BindConfiguration(nameof(ClientSecretOption))
              .ValidateDataAnnotations()
              .ValidateOnStart();

            services.AddSingleton<ClientSecretOption>(sp =>
                sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<ClientSecretOption>>().Value);








            services.AddRefitClient<IPaymentService>()
            .ConfigureHttpClient(c =>
            {

                var addressUrlOption = configuration.GetSection(nameof(AddressUrlOption)).Get<AddressUrlOption>();
                c.BaseAddress = new Uri(addressUrlOption!.PaymentUrl);
            }).AddHttpMessageHandler<AuthenticatedHtttpClientHandler>().AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();


            return services;


        }



    }
}
