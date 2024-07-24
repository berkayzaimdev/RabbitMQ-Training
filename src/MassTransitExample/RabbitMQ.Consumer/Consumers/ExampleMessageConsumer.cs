using MassTransit;
using RabbitMQ.Shared.Messages;

namespace RabbitMQ.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen mesaj: {context.Message}");
            return Task.CompletedTask;
        }
    }
}
