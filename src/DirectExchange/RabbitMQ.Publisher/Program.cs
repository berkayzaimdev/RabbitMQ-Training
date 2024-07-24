using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct); // adı ve türü ile consumer'ın aynısı olmalı

while (true)
{
    Console.Write("Mesaj: ");
    string message = Console.ReadLine();

    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "direct-exchange-example", 
        routingKey: "direct-queue-example", 
        body: byteMessage);
}