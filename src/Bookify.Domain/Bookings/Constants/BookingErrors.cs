using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Constants;

public static class BookingErrors
{
    public static readonly Error NotFound = new(
        Code: "Booking.NotFound",
        Name: "The booking with the specified identifier was not found");

    public static readonly Error Overlap = new(
        Code: "Booking.Overlap",
        Name: "The current booking is overlapping with an existing one");

    public static readonly Error NotReserved = new(
        Code: "Booking.NotReserved",
        Name: "The booking is not pending");

    public static readonly Error NotConfirmed = new(
        Code: "Booking.NotConfirmed",
        Name: "The booking is not confirmed");

    public static readonly Error AlreadyStarted = new(
        Code: "Booking.AlreadyStarted",
        Name: "The booking has already started");
}