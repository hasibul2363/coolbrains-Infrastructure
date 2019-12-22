using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Bus
{
    public class DefaultTopicClient : ITopicClient
    {
        public Task PublishAsync<TMessage>(TMessage message) where TMessage : IBusTopicMessage
        {
            throw new NotImplementedException();
        }
    }
}
