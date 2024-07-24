using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "example-work-queue";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false
    );

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes($"merhaba {i}");

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: queueName,
        body: message
        );
}

