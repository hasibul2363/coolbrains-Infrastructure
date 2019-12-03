using System;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Bus
{
    public class BusMessageDispatcher : IBusMessageDispatcher
    {
        public Task DispatchAsync<TMessage>(TMessage message) where TMessage : IBusMessage
        {
            throw new NotImplementedException();
        }
    }
}
