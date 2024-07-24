
using MassTransit;
using RabbitMQ.Shared.RequestResponseMessages;

string rabbitMQUri = "amqps://xncluoek:Pg5sa3Y0HAPsVkv0ox0b6y8qi1zObKaD@moose.rmq.cloudamqp.com/xncluoek";

string queueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{queueName}"));

int i = 1;

while (true)
{
    await Task.Delay(200);
    var response = await request.GetResponse<ResponseMessage>(new RequestMessage()
    {
        MessageNo = 1,
        Text = $"{i}. request"
    });

    Console.WriteLine($"Response message: {response.Message.Text}");
}
