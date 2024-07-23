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
channel.QueueDeclare(queue: "sample-queue", exclusive: false, durable:true); // publisher'daki ile BİREBİR AYNI tanımlama olmalıdır.

// Kuyruktan mesaj okuma
EventingBasicConsumer consumer = new(channel);
var consumerTag = channel.BasicConsume(queue: "sample-queue", autoAck: false, consumer: consumer); // autoAck, mesajı alınca mesajın silinip silinmemesini belirleyecek parametredir.
// channel.BasicCancel(consumerTag); // kuyruktaki tüm mesajların işlenmesini reddetme
// channel.BasicReject(deliveryTag:3, requeue: true); // tek bir mesajın işlenmesini reddetme

channel.BasicQos(0, 1, false);  // prefetchSize: consumer tarafından alınabilecek en büyük mesaj boyutunun byte cinsinden ifadesi
                                // prefetchCount: consumer tarafından aynı anda işleme alınabilecek mesaj sayısı
                                // global: tüm consumer'lar mı yoksa sadece çağrı yapılan consumer mı


consumer.Received += async (sender, e) =>
{
    // kuyruğa gelen mesajın işlendiği yerdir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    await Task.Delay(3000);
    //... process ...

    channel.BasicAck(e.DeliveryTag, multiple: false); // mesajın işlendiğine dair uyarı bu metot ile gönderilir. multiple parametresini, önceki mesajların da işlendiğini onaylamak istiyorsak true yapabiliriz.

    // channel.BasicNack(e.DeliveryTag, multiple: false, requeue: true); // istenmeyen durumlarda bu metot ile RabbitMQ'ya bilgi verip mesajı tekrardan işletebiliriz. requeue parametresi, işlenemeyeceği ifade edilen bu mesajın tekrardan kuyruğa eklenip eklenmeyeceğini belirtir.
};
// RabbitMQ, kuyruğa atacağı mesajları byteArr olarak kabul etmektedir.