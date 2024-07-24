using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic); // buralar klasik artık, publisher'ın aynısı olmalı

Console.Write("Dinlenecek topic'i belirtiniz: ");
string topic = Console.ReadLine();
string queueName = channel.QueueDeclare().QueueName; // detayları çok önemli değil şuan

channel.QueueBind(
    queue: queueName,
    exchange: "topic-exchange-example",
    routingKey: topic); // topic'e göre mesajları dağıtır


EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


