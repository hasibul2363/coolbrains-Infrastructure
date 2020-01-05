using System.Collections.Generic;
using System.Runtime.Serialization;
using CoolBrains.Infrastructure.Events;
using Newtonsoft.Json;

namespace CoolBrains.Infrastructure.Commands
{
    public class CommandResponse
    {
        [IgnoreDataMember]
        [JsonIgnore]
        public IEnumerable<IEvent> Events { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public object Result { get; set; }
    }

    
}
