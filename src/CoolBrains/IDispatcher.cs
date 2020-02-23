using System.Threading.Tasks;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Queries;

namespace CoolBrains.Infrastructure
{
    public interface IDispatcher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;

        Task<TResult> GetResultAsync<TResult>(IQuery<TResult> query);
        TResult GetResult<TResult>(IQuery<TResult> query);

        CommandResponse Send(ICommand command);
        Task<CommandResponse> SendAsync(ICommand command);

        Task SendBusMessageAsync<TMessage>(TMessage message) where TMessage : IMessage;
        Task PublishBusMessageAsync<TMessage>(TMessage message) where TMessage : IMessage;
    }
}
