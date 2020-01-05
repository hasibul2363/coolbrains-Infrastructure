namespace CoolBrains.Infrastructure.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        CommandResponseWithEvents Handle(TCommand command);
    }
}
