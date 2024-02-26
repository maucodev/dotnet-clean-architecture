using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Constants;

public static class BookingErrors
{
    public static readonly Error NotFound = new(
        Code: "Booking.NotFound",
        Description: "The booking with the specified identifier was not found");

    public static readonly Error Overlap = new(
        Code: "Booking.Overlap",
        Description: "The current booking is overlapping with an existing one");

    public static readonly Error NotReserved = new(
        Code: "Booking.NotReserved",
        Description: "The booking is not pending");

    public static readonly Error NotConfirmed = new(
        Code: "Booking.NotConfirmed",
        Description: "The booking is not confirmed");

    public static readonly Error AlreadyStarted = new(
        Code: "Booking.AlreadyStarted",
        Description: "The booking has already started");
}