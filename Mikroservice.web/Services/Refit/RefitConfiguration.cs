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


            services.AddRefitClient<ICatalogRefitService>()
            .ConfigureHttpClient(c =>
            {
                var microserviceOption = configuration.GetSection(nameof(MicroserviceOption)).Get<MicroserviceOption>();
                c.BaseAddress = new Uri("http://microservice-catalog-api");
            })
            .AddHttpMessageHandler<AuthenticatedHtttpClientHandler>()//bu usertoken için istek atarken kullanmak için
            .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>()//bu clientcredential için token alıp istek göndermek için
            ;


            return services;


        }



    }
}
