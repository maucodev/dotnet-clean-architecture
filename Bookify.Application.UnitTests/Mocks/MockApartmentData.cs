using Bookify.Domain.Apartments.Entity;
using Bookify.Domain.Shared;
using System;

namespace Bookify.Application.UnitTests.Mocks;

internal static class MockApartmentData
{
    public static Apartment Create() => new(
        Guid.NewGuid(),
        new Name("Test apartment"),
        new Description("Test description"),
        new Address("Country", "State", "ZipCode", "City", "Street"),
        new Money(100.0m, Currency.Usd),
        Money.Zero(),
        []);
}