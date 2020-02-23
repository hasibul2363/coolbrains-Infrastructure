using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Domain;

namespace CoolBrains.Infrastructure.Events
{
    public interface IDomainEventProcessor
    {
        Task Process(IEnumerable<IEvent> events, ICommand command);
    }
}
