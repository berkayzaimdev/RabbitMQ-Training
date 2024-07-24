using MassTransit;
using RabbitMQ.Shared.Messages;

string rabbitMQUri = "amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek";

string queueName = "example-masstransit-queue";

var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}")); // mesaj buraya gidecek

Console.Write("gönderilecek mesaj: ");
string message = Console.ReadLine();
await sendEndpoint.Send<IMessage>(new ExampleMessage() // kuyruğa özel mesaj göndermeyi sağlayan operasyon
{
    Text = message
});

Console.ReadLine();