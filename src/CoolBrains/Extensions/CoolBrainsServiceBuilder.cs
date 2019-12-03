using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CoolBrains.Infrastructure.Extensions
{
    public class CoolBrainsServiceBuilder : ICoolBrainsServiceBuilder
    {
        public IServiceCollection Services { get; }

        public CoolBrainsServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}
