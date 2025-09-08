using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservice.Catalog.Api.Options
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
