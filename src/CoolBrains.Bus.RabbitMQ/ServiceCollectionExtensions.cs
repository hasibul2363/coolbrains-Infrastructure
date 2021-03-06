﻿using System;
using CoolBrains.Infrastructure.Extensions;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoolBrains.Infrastructure.Bus.RabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        public static ICoolBrainsServiceBuilder AddRabbitMqProvider(this ICoolBrainsServiceBuilder builder, IConfiguration configuration)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.Services.AddMassTransit();
            builder.Services.Configure<RabbitConfig>(configuration.GetSection("RabbitConfig"))
                .AddScoped<IBusMessageDispatcher, BusMessageDispatcher>()
                .AddScoped<ITopicClient, TopicClient>()
                .AddScoped<IQueueClient, QueueClient>();

            return builder;
        }
    }
}
