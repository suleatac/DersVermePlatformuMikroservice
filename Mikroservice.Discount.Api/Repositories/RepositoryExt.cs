using Mikroservice.Discount.Api.Options;
using MongoDB.Driver;

namespace Mikroservice.Discount.Api.Repositories
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddDatabaseServiceExt(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetRequiredService<MongoOption>();
                return new MongoClient(options.ConnectionString);
            });

            services.AddScoped(sp =>
            {
                var mongoclient = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<MongoOption>();
                var database = mongoclient.GetDatabase(options.DatabaseName);
                return AppDbContext.Create(database);
            });





            return services;
        }
    }
}
