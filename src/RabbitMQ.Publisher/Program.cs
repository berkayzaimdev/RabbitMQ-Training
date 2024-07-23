using RabbitMQ.Client;
using System.Text;

// Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek"); // cloud manager'da amqp kısmındaki url

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection(); // disposable olduğu için using, bağlantıyı sağladık
using IModel channel = connection.CreateModel(); // disposable olduğu için using, kanal oluşturduk

// Kuyruk oluşturma
channel.QueueDeclare(queue: "sample-queue", exclusive: false, durable:true); // exclusive true olsaydı, consumer bu kuyruğa erişemeden bu kuyruk silinmiş olacaktı.
                                                                             // durable property'si ise kuyruğu kalıcı hale getirmeyi sağlar.
                                                                             // durable property, hem publisher'da hem de consumer'da aynı olmalıdır!

IBasicProperties properties = channel.CreateBasicProperties();

// Kuyruğa mesaj gönderme
for (int i = 0; i < 120; i++)
{
    var message = Encoding.UTF8.GetBytes("Hello world "+i);
    channel.BasicPublish(exchange: "", routingKey: "sample-queue", body: message, basicProperties: properties);

}

Console.Read();

// RabbitMQ, kuyruğa atacağı mesajları byteArr olarak kabul etmektedir.