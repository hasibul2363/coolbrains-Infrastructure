using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Options;

namespace CoolBrains.Infrastructure.Bus.RabbitMQ
{
    public class QueueClient : IQueueClient
    {
        private readonly RabbitConfig _rabbitConfig;
        private IBusControl _busControl;
        public QueueClient(IOptions<RabbitConfig> rabbitConfig)
        {
            _rabbitConfig = rabbitConfig.Value;
        }

        private string GetQueueName(IBusQueueMessage message)
        {
            if (message.QueueName == string.Empty)
                return message.GetType().Namespace;
            return message.QueueName;
        }

        public async Task SendAsync<TMessage>(TMessage message) where TMessage : IBusQueueMessage
        {
            _busControl = _busControl ?? BuildBus();
            var queueName = GetQueueName(message);
            var sendToUri = new Uri($"{_rabbitConfig.ConnectionString}/{queueName}");
            var endPoint = await _busControl.GetSendEndpoint(sendToUri);
            await endPoint.Send(message);
        }

        public IBusControl BuildBus()
        {
            return MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(_rabbitConfig.ConnectionString), h =>
                {
                    h.Username(_rabbitConfig.UserName);
                    h.Password(_rabbitConfig.Password);
                });
            });

        }
    }
}
