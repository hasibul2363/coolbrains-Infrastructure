using System.Collections.Generic;
using System.Runtime.Serialization;
using CoolBrains.Infrastructure.Events;
using Newtonsoft.Json;

namespace CoolBrains.Infrastructure.Commands
{
    public class CommandResponse
    {
        public CommandResponse(ValidationResult validationResult, object result)
        {
            ValidationResult = validationResult;
            Result = result;
        }
        public ValidationResult ValidationResult { get; set; }
        public object Result { get; set; }
    }

    public class CommandResponseWithEvents
    {
        public ValidationResult ValidationResult { get; set; }
        public object Result { get; set; }
        public IEnumerable<IEvent> Events { get; set; }
    }


}
