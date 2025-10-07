using MassTransit;
using Microservice.Bus;
using Microservice.File.Api.Consumers;

namespace Microservice.File.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = (configuration.GetSection(nameof(BusOption)).Get<BusOption>())!;
            services.AddMassTransit(configure =>
            {

                configure.AddConsumer<UploadCoursePictureCommandConsumer>();


                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), h =>
                    {
                        h.Username(busOptions.UserName);
                        h.Password(busOptions.Password);
                    });

                    // cfg.ConfigureEndpoints(context);//hangi consumer ın hangi queue yu dinleyeceğini otomatik yapıyor

                    cfg.ReceiveEndpoint("file-microservice.upload-course-picture-command.queue", e =>
                                        {
                                            e.ConfigureConsumer<UploadCoursePictureCommandConsumer>(context);

                                        });
                });




            });

            return services;

        }
    }
}
