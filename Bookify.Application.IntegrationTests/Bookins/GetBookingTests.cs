﻿using Bookify.Application.Bookings.GetBooking;
using Bookify.Application.IntegrationTests.Infrastructure;
using Bookify.Domain.Bookings.Entity;
using System.Threading.Tasks;
using System;
using FluentAssertions;

namespace Bookify.Application.IntegrationTests.Bookins;

public class GetBookingTests : BaseIntegrationTest
{
    private static readonly Guid BookingId = Guid.NewGuid();

    public GetBookingTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetBooking_ShouldReturnFailure_WhenBookingIsNotFound()
    {
        // Arrange
        var query = new GetBookingQuery(BookingId);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Error.Should().Be(BookingErrors.NotFound);
    }
}