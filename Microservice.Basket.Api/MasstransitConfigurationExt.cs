using MassTransit;
using Microservice.Basket.Api.Consumers;
using Microservice.Bus;
namespace Microservice.Basket.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = (configuration.GetSection(nameof(BusOption)).Get<BusOption>())!;
            services.AddMassTransit(configure =>
            {

                configure.AddConsumer<OrderCreatedEventConsumer>();


                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), h =>
                    {
                        h.Username(busOptions.UserName);
                        h.Password(busOptions.Password);
                    });

                    // cfg.ConfigureEndpoints(context);//hangi consumer ın hangi queue yu dinleyeceğini otomatik yapıyor

                    cfg.ReceiveEndpoint("basket-microservice.order-created-event.queue", e =>
                                        {
                                            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);

                                        });
                });




            });

            return services;

        }
    }
}
