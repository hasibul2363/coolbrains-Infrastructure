using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Bus
{
    public interface IBusMessageDispatcher
    {
        Task DispatchAsync<TMessage>(TMessage message) where TMessage : IMessage;
    }
}
