using CoolBrains.Infrastructure.Bus;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

// ReSharper disable StringLiteralTypo

namespace CoolBrains.Bus.RabbitMQ
{
    public class TopicClient : ITopicClient
    {
        private readonly RabbitConfig _rabbitConfig;
        private IBusControl _busControl;
        public TopicClient(IOptions<RabbitConfig> rabbitConfig)
        {
            _rabbitConfig = rabbitConfig.Value;
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

        public Task PublishAsync<TMessage>(TMessage message) where TMessage : IBusTopicMessage
        {
            _busControl = _busControl ?? BuildBus();
            return _busControl.Publish(message);
        }
    }
}