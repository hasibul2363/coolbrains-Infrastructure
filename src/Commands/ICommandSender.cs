using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public interface ICommandSender
    {
        Task<CommandResponse> SendAsync(ICommand command);
    }
}
