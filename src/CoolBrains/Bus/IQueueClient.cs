using System;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Bus
{
    public interface IQueueClient
    {
        Task SendAsync<TMessage>(TMessage message) where TMessage : IBusQueueMessage;
    }
}
