using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Domain.Events
{
    public class DomainEvent: Event, IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateRootId { get; set; }
    }
}
