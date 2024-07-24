using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i + 1}");

    Console.Write("Mesajın gönderileceği topic formatını belirtiniz: ");
    string topic = Console.ReadLine();

    channel.BasicPublish(
        exchange: "topic-exchange-example",
        routingKey: topic, // hangi topic'e göre mesaj göndereceğiz? burada belirliyoruz
        body: message);
}