using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Bus
{
    public interface ITopicClient
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : IBusTopicMessage;
    }

}
