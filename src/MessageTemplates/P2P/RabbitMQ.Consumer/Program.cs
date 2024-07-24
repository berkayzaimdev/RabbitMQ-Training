using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "example-p2p-queue";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: false,
    consumer: consumer
    );
// tek bir consumer, tek bir queue'dan mesaj tüketecek

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};