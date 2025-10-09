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
            return services;
        }



    }
}
