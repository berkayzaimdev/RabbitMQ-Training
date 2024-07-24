using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Publisher.Services;

IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddMassTransit(configurator =>
    {
        configurator.UsingRabbitMq((context, _configurator) =>
        {
            _configurator.Host("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek"); // her zaman kullandığımız connectionstring gibi olan rabbitmq bağlantısı

            services.AddHostedService<PublishMessageService>();
        });
    });
}).Build();

host.Run();