using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Commands
{
    public interface ICommandHandlerAsync<in TCommand> where TCommand : ICommand
    {
        Task<CommandResponseWithEvents> HandleAsync(TCommand command);
    }
}
