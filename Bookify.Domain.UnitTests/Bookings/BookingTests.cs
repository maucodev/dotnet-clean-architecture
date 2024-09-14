using Bookify.Domain.Bookings.Entity;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Bookings.Services;
using Bookify.Domain.Shared;
using Bookify.Domain.Users.Entity;
using System;
using Bookify.Domain.UnitTests.Infrastructure;
using Bookify.Domain.UnitTests.Mocks;
using FluentAssertions;

namespace Bookify.Domain.UnitTests.Bookings;

public class BookingTests : BaseTest
{
    [Fact]
    public void Reserve_Should_RaiseBookingReservedDomainEvent()
    {
        // Arrange
        var user = User.Create(MockUserData.FirstName, MockUserData.LastName, MockUserData.Email);
        var price = new Money(10.0m, Currency.Usd);
        var duration = DateRange.Create(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));
        var apartment = MockApartmentData.Create(price);
        var pricingService = new PricingService();

        // Act
        var booking = Booking.Reserve(apartment, user.Value.Id, duration, DateTime.UtcNow, pricingService);

        // Assert
        var bookingReservedDomainEvent = AssertDomainEventWasPublished<BookingReservedDomainEvent>(booking.Value);

        bookingReservedDomainEvent.BookingId.Should().Be(booking.Value.Id);
    }
}