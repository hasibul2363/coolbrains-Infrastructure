using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Domain;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Store.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace CoolBrains.Infrastructure.Extensions
{
    public static  class ServiceCollectionExtensions
    {
        public static void AddCoolBrains(this IServiceCollection services, params Type[] types)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //var typeList = types.ToList();
            //typeList.Add(typeof(IDispatcher));

            
            
            
            
            services.AddScoped(typeof(IDomainRepository<>), typeof(DomainRepository<>));
            services.AddScoped<IBusMessageDispatcher, BusMessageDispatcher>();
            services.AddScoped<ICommandSender, CommandSender>();
            services.AddScoped<IEventPublisher, EventPublisher>();


            /*
            IEventHandlerAsync<>
            ICommandHandlerAsync<>
            ICommandHandler<>
                */

            
            

           

        }
    }
}
