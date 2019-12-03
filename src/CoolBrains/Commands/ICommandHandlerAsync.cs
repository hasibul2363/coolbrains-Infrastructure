using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Commands
{
    public interface ICommandHandlerAsync<in TCommand> where TCommand : ICommand
    {
        Task<CommandResponse> HandleAsync(TCommand command);
    }
}
