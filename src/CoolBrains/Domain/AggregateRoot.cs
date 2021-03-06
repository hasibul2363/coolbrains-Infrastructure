﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ReflectionMagic;

namespace CoolBrains.Infrastructure.Domain
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public int Version { get; private set; }

        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
        public ReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

        protected AggregateRoot()
        {
            Id = Guid.NewGuid();
        }

        protected AggregateRoot(Guid id)
        {
            if (id == Guid.Empty)
                id = Guid.NewGuid();

            Id = id;
        }

        public void LoadsFromHistory(IEnumerable<IDomainEvent> events)
        {
            var domainEvents = events as IDomainEvent[] ?? events.ToArray();
            if (domainEvents.Any())
                Id = domainEvents.First().AggregateRootId;
            
            foreach (var @event in domainEvents)
            {
                ApplyEvent(@event);
            }
        }

        protected void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        private void ApplyEvent(IDomainEvent @event)
        {
            this.AsDynamic().Apply(@event);
            Version++;
        }

        protected void AddAndApplyEvent<T>(IDomainEvent @event) where T: IAggregateRoot
        {
            //@event.AggregateRootId = this.Id;
            @event.Source = typeof(T).FullName;
            @event.TimeStamp = DateTime.UtcNow;
            AddEvent(@event);
            ApplyEvent(@event);
        }
    }
}
