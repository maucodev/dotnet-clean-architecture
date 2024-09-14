using Bookify.Domain.Apartments.Entity;
using Bookify.Domain.Shared;
using System;

namespace Bookify.Domain.UnitTests.Mocks;

internal static class MockApartmentData
{
    public static Apartment Create(Money price, Money cleaningFee = null) => new(
        Guid.NewGuid(),
        new Name("Test apartment"),
        new Description("Test description"),
        new Address("Country", "State", "ZipCode", "City", "Street"),
        price,
        cleaningFee ?? Money.Zero(),
        []);
}