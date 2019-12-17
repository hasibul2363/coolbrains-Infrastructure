using System.Collections.Generic;
using CoolBrains.Infrastructure.Events;

namespace CoolBrains.Infrastructure.Commands
{
    public class CommandResponse
    {
        public IEnumerable<IEvent> Events { get; set; } = new List<IEvent>();
        public ValidationResult ValidationResult { get; set; }
        public object Result { get; set; }
    }

    
}
