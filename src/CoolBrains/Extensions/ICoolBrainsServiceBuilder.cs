using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CoolBrains.Infrastructure.Extensions
{
    public interface ICoolBrainsServiceBuilder
    {
        IServiceCollection Services { get; }
    }
}
