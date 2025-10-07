using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Bus
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddCommonMasstransitExt(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions=( configuration.GetSection(nameof(BusOption)).Get<BusOption>())!;
            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), h =>
                    {
                        h.Username(busOptions.UserName);
                        h.Password(busOptions.Password);
                    });

                    cfg.ConfigureEndpoints(context);



                });
            });



            return services;
        }
    }
}
