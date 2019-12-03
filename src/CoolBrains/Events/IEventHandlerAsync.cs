using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Events
{
    public interface IEventHandlerAsync<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
