using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Commands
{
    public interface ICommandSender
    {
        Task<CommandResponse> SendAsync(ICommand command);
        CommandResponse Send(ICommand command);
    }
}
