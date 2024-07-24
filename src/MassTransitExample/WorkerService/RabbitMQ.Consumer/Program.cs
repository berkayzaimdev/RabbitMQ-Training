using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Consumer.Consumers;
using RabbitMQ.Publisher.Services;

IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddMassTransit(configurator =>
    {
        configurator.UsingRabbitMq((context, _configurator) =>
        {
            _configurator.Host("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek");

            _configurator.ReceiveEndpoint("example-message-queue", e => e.ConfigureConsumer<ExampleMessageConsumer>(context)); // kuyruk ismi veririz
        });

        services.AddHostedService<PublishMessageService>();
    });
}).Build();

host.Run();