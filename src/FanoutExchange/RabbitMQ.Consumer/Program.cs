using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "fanout-exchange-example",
    type: ExchangeType.Fanout);

Console.Write("Kuyruk adını giriniz: ");
string _queueName = Console.ReadLine();

channel.QueueDeclare(
    queue: _queueName, 
    exclusive: false // bu kuyruğa başka bağlantılar olabilir o yüzden false
    );

channel.QueueBind( // bir exchange ile kuyrukların ilişkilendirilmesi, bind işlemi
    queue: _queueName,
    exchange: "fanout-exchange-example",
    routingKey: string.Empty // fanout exchange olduğu için routing key boş olmalı
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: _queueName,
    autoAck: true,
    consumer: consumer);


consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};