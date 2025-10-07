using MassTransit;
using Microservice.Bus;
using Microservice.Bus.Events;
using Microservice.Catalog.Api.Consumer;

namespace Microservice.Catalog.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = (configuration.GetSection(nameof(BusOption)).Get<BusOption>())!;
            services.AddMassTransit(configure =>
            {

                configure.AddConsumer<CoursePictureUploadedEventConsumer>();


                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), h =>
                    {
                        h.Username(busOptions.UserName);
                        h.Password(busOptions.Password);
                    });

                    // cfg.ConfigureEndpoints(context);//hangi consumer ın hangi queue yu dinleyeceğini otomatik yapıyor

                    cfg.ReceiveEndpoint("catalog-microservice.course-picture-uploaded.queue", e =>
                                        {
                                            e.ConfigureConsumer<CoursePictureUploadedEventConsumer>(context);

                                        });
                });




            });

            return services;

        }
    }
}
