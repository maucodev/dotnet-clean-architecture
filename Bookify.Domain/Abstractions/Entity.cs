﻿using System;
using System.Collections.Generic;

namespace Bookify.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public Guid Id { get; init; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}