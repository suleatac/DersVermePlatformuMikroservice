using Microservice.web.DelegateHandlers;
using Mikroservice.web.Options;
using Mikroservice.web.Services.Refit.CatalogService;
using Refit;

namespace Microservice.web.Services.Refit
{
    public static class RefitConfiguration
    {

        public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthenticatedHtttpClientHandler>();

            services.AddScoped<ClientAuthenticatedHttpClientHandler>();


            services.AddOptions<GatewayOption>()
                .BindConfiguration(nameof(GatewayOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<GatewayOption>(sp =>
                sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<GatewayOption>>().Value);









            services.AddRefitClient<ICatalogRefitService>()
            .ConfigureHttpClient(c =>
            {

                var gatewayOption = configuration.GetSection(nameof(GatewayOption)).Get<GatewayOption>();
                c.BaseAddress = new Uri(gatewayOption!.BaseAddress);
            })
            .AddHttpMessageHandler<AuthenticatedHtttpClientHandler>()//bu usertoken için istek atarken kullanmak için
            .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>()//bu clientcredential için token alıp istek göndermek için
            ;


            return services;


        }



    }
}
