using System.Collections.Generic;
using System.Runtime.Serialization;
using CoolBrains.Infrastructure.Events;
using Newtonsoft.Json;

namespace CoolBrains.Infrastructure.Commands
{
    public class CommandResponse
    {
        public ValidationResult ValidationResult { get; set; }
        public object Result { get; set; }
    }

    public class CommandResponseWithEvents : CommandResponse
    {
        public IEnumerable<IEvent> Events { get; set; }
    }


}
