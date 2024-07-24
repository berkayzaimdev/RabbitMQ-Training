using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// 1.Adım
channel.ExchangeDeclare(
    exchange: "direct-exchange-example", 
    type: ExchangeType.Direct); // adı ve türü ile publisher'ın aynı olmalı

// 2.Adım 
var queueName = channel.QueueDeclare().QueueName; // oluşturduğumuz queue'ya isim vermezsek random oluşur. bu random ismi elde ettik

//3.Adım
channel.QueueBind(
    queue: queueName, 
    exchange: "direct-exchange-example", 
    routingKey: "direct-queue-example");

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

// 1.Adım: Publisher'daki exchange ile birebir aynı isim ve türe sahip bir exchange tanımlanmalıdır.
// 2.Adım: Publisher tarafından routing key'de bulunan değerdeki kuyruğa gönderilen mesajları kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemiz gerekmektedir. Bunun için öncelikle bir kuyruk oluşturulmalıdır.
// 3.Adım: Parametreleri önceki queueName, exchange ve routing-key tanımlamalarımızla örtüşen bir kuyruk tanımlarız ve consume işlemini uygularız.