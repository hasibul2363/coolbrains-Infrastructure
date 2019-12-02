using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        CommandResponse Handle(TCommand command);
    }
}
