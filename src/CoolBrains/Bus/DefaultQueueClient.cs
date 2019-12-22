using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Bus
{
    class DefaultQueueClient : IQueueClient
    {
        public Task SendAsync<TMessage>(TMessage message) where TMessage : IBusQueueMessage
        {
            throw new NotImplementedException();
        }
    }
}
