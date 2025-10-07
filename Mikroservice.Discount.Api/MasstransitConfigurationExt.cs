using Microservice.Bus;
using Microservice.Discount.Api.Consumers;
namespace Microservice.Discount.Api
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

                    cfg.ReceiveEndpoint("discount-microservice.order-created-event.queue", e =>
                                        {
                                            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);

                                        });
                });




            });

            return services;

        }
    }
}
