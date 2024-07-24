using RabbitMQ.Client;
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
    ); // tek bir queue'ya göndeririz

byte[] message = Encoding.UTF8.GetBytes("merhaba");

channel.BasicPublish( // exchange'e gerek yoktur.
    exchange: string.Empty,
    routingKey: queueName,
    body: message
    );