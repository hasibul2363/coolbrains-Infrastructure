namespace CoolBrains.Infrastructure.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        CommandResponse Handle(TCommand command);
    }
}
