using Microsoft.Extensions.Options;

namespace Mikroservice.Discount.Api.Options
{
    public static class OptionExt
    {
        public static IServiceCollection AddMongoOptionExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOption>()
                .BindConfiguration(nameof(MongoOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();


            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value);






            return services;
        }
    }
}
