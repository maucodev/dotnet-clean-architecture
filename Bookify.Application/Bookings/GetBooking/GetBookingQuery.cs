using System;
using Bookify.Application.Caching;

namespace Bookify.Application.Bookings.GetBooking;

public sealed record GetBookingQuery(Guid BookingId) : ICachedQuery<BookingResponse>
{
    public string CacheKey => $"bookings:get-booking:{BookingId}";

    public TimeSpan Expiration => TimeSpan.FromSeconds(30);
}