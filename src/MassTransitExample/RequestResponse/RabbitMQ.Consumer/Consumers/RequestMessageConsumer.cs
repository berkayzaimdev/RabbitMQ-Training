using MassTransit;
using RabbitMQ.Shared.RequestResponseMessages;

namespace RabbitMQ.Consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            Console.WriteLine($"Request alındı: {context.Message.Text}");
            await context.RespondAsync<ResponseMessage>(new() 
            {
                Text = $"{context.Message.MessageNo}. response to request"
            });
        }
    }

    // public class ResponseMessageConsumer : IConsumer<ResponseMessage> // tanımlamadık çünkü bu zaten publisher projesinde var metotlar ile kontrol sağladık
}
