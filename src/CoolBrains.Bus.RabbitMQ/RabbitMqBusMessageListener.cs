using System;
using System.Collections.Generic;
using CoolBrains.Infrastructure.Extensions;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CoolBrains.Infrastructure.Bus.RabbitMQ
{
    public static class RabbitMqBusMessageListener
    {
        private static RabbitConfig _rabbitConfig;
        private static IBusControl _busControl;
        public static IRabbitMqHost _rabbitMqHost;

        private static Dictionary<string, Action<IRabbitMqReceiveEndpointConfigurator>> _queueConfigurations = new Dictionary<string, Action<IRabbitMqReceiveEndpointConfigurator>>();
        public static ICoolBrainsServiceBuilder Listen(this ICoolBrainsServiceBuilder builder, Action<IRabbitMqReceiveEndpointConfigurator> queueConfiguration = null)
        {
            _rabbitConfig = builder.Services.BuildServiceProvider().GetService<IOptions<RabbitConfig>>().Value;
            _queueConfigurations.Add(_rabbitConfig.QueueName, queueConfiguration);
            return builder;
        }

        private static void Initialize()
        {
            _busControl = MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                _rabbitMqHost = cfg.Host(_rabbitConfig.ConnectionString, h =>
                {
                    h.Username(_rabbitConfig.UserName);
                    h.Password(_rabbitConfig.Password);
                });

                foreach (var queueConfiguration in _queueConfigurations)
                {
                    cfg.ReceiveEndpoint(queueConfiguration.Key, queueConfiguration.Value);
                }


                //if (RetryCount > 0)
                //{
                //    cfg.UseRetry(config =>
                //    {
                //        config.Interval(RetryCount, TimeSpan.FromSeconds(RetryIntervalInSec));
                //    });
                //}

                //cfg.UseSerilog();
            });
        }


        public static void Start(this ICoolBrainsServiceBuilder builder)
        {
            Initialize();
            _busControl.Start();
        }

        public static void Stop()
        {

            _busControl.Stop();

        }
    }
}
