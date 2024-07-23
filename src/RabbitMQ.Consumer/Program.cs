using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek"); // cloud manager'da amqp kısmındaki url

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection(); // disposable olduğu için using, bağlantıyı sağladık
using IModel channel = connection.CreateModel(); // disposable olduğu için using, kanal oluşturduk

// Kuyruk oluşturma
channel.QueueDeclare(queue: "sample-queue", exclusive: false); // publisher'daki ile BİREBİR AYNI tanımlama olmalıdır.

// Kuyruktan mesaj okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "sample-queue", autoAck: false, consumer: consumer); // autoAck, mesajı alınca mesajın silinip silinmemesini belirleyecek parametredir.

consumer.Received += (sender, e) =>
{
    // kuyruğa gelen mesajın işlendiği yerdir.
    var message = e.Body.Span;
    Console.WriteLine(Encoding.UTF8.GetString(message));
};

// RabbitMQ, kuyruğa atacağı mesajları byteArr olarak kabul etmektedir.