using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Dependencies;
using CoolBrains.Infrastructure.Domain;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Queries;
using CoolBrains.Infrastructure.Store.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace CoolBrains.Infrastructure.Extensions
{
    public static  class ServiceCollectionExtensions
    {
        public static ICoolBrainsServiceBuilder AddCoolBrains(this IServiceCollection services, params Type[] types)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //var typeList = types.ToList();
            //typeList.Add(typeof(IDispatcher));



            services.AddSingleton<IResolver, Resolver>();
            services.AddSingleton<IHandlerResolver, HandlerResolver>();
            services.AddSingleton<IDispatcher, Dispatcher>();
            services.AddScoped(typeof(IDomainRepository<>), typeof(DomainRepository<>));
            services.AddScoped<IBusMessageDispatcher, BusMessageDispatcher>();
            services.AddScoped<ICommandSender, CommandSender>();
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();
            services.AddSingleton<IQueueClient, DefaultQueueClient>();
            services.AddSingleton<ITopicClient, DefaultTopicClient>();


            /*
            IEventHandlerAsync<>
            ICommandHandlerAsync<>
            ICommandHandler<>
                */





            return new CoolBrainsServiceBuilder(services);
        }
    }
}
