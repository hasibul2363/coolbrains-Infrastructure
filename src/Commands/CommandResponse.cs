using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Domain.Events;

namespace Commands
{
    public class CommandResponse
    {
        public IEnumerable<IEvent> Events { get; set; } = new List<IEvent>();
        public ValidationResult ValidationResult { get; set; }
        public object Result { get; set; }
    }
}
