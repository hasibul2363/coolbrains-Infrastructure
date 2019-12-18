using System;
using System.Collections.Generic;
using CoolBrains.Infrastructure.Events;
using Microsoft.Extensions.DependencyInjection;

namespace CoolBrains.Infrastructure.Dependencies
{
    public class Resolver : IResolver
    {
        private IServiceProvider _serviceProvider;

        public Resolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Resolve<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            try
            {
                var services = _serviceProvider.GetServices(typeof(IEventHandlerAsync<>));
            }
            catch (Exception ex)
            {

                throw;
            }
            return _serviceProvider.GetServices<T>();
        }

        public object Resolve(Type type)
        {
            return _serviceProvider.GetService(type);
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return _serviceProvider.GetServices(type);
        }
    }
}
