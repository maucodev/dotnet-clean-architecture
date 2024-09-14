using Bookify.Domain.Abstractions;
using System;
using System.Linq;

namespace Bookify.Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
    protected static T AssertDomainEventWasPublished<T>(Entity entity) where T : IDomainEvent
    {
        var domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault();

        if (domainEvent is null)
        {
            throw new Exception($"{typeof(T).Name} was not published");
        }

        return domainEvent;
    }
}