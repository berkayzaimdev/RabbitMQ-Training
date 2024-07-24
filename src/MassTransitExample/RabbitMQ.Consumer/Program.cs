using MassTransit;
using RabbitMQ.Consumer.Consumers;

string rabbitMQUri = "amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek";

string queueName = "example-masstransit-queue";

var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(queueName: queueName, configureEndpoint: endpoint => 
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});

