using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string exchangeName = "example-pub-sub-exchange";

channel.ExchangeDeclare(
    exchange: exchangeName,
    type: ExchangeType.Fanout // pub-sub davranışına uygun olarak, fanout exchange biçilmiş kaftandır. abone olan tüm kuyruklara mesaj gönderir
    );

string queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(
    queue: queueName,
    exchange: exchangeName,
    routingKey: string.Empty
    );

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
    queue: queueName,
    consumer: consumer,
    autoAck: false
    );

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};