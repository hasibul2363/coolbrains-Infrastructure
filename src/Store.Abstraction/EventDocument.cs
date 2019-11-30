using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Store.Abstraction
{
    public class EventDocument
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public long Sequence { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid UserId { get; set; }

    }
}
