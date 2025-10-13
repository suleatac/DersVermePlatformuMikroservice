using Microsoft.Extensions.Options;
using Mikroservice.web.Options;

namespace Mikroservice.web.Extentions
{
    public static class OptionsExt
    {

        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
            services.AddOptions<IdentityOption>()
                .BindConfiguration(nameof(IdentityOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton(sp=>sp.GetRequiredService<IOptions<IdentityOption>>().Value);

            services.AddOptions<GatewayOption>()
                .BindConfiguration(nameof(GatewayOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddSingleton(sp=>sp.GetRequiredService<IOptions<GatewayOption>>().Value);



            services.AddOptions<MicroserviceOption>()
                .BindConfiguration(nameof(MicroserviceOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddSingleton(sp=>sp.GetRequiredService<IOptions<MicroserviceOption>>().Value);



            return services;
        }



    }
}
