using RabbitMQ.Client;
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

for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes($"merhaba {i}");

    channel.BasicPublish(
        exchange: exchangeName,
        routingKey: string.Empty,
        body: message
        );
}

