using MassTransit;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Shared.Messages;

namespace RabbitMQ.Publisher.Services
{
    public class PublishMessageService : BackgroundService
    {
        readonly IPublishEndpoint _publishEndpoint;

        public PublishMessageService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (true)
            {
                ExampleMessage message = new()
                {
                    Text = $"{++i} message"
                };

               await _publishEndpoint.Publish(message); 
                // publish tüm kuyruklara mesaj yollarken, send ise belirli bir kuyruğa mesaj yollar
            }   
        }
    }
}
