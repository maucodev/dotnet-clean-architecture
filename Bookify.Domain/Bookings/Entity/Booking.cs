using System;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments.Entity;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Bookings.Services;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings.Entity;

public sealed class Booking : Abstractions.Entity
{
    private Booking()
    {
    }

    private Booking(
        Guid id,
        Guid apartmentId,
        Guid userId,
        DateRange duration,
        Money priceForPeriod,
        Money cleaningFee,
        Money amenitiesUpCharge,
        Money totalPrice,
        BookingStatus status,
        DateTime created) : base(id)
    {
        ApartmentId = apartmentId;
        UserId = userId;
        Duration = duration;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        AmenitiesUpCharge = amenitiesUpCharge;
        TotalPrice = totalPrice;
        Status = status;
        Created = created;
    }

    public Guid ApartmentId { get; private set; }

    public Guid UserId { get; private set; }

    public DateRange Duration { get; private set; }

    public Money PriceForPeriod { get; private set; }

    public Money CleaningFee { get; private set; }

    public Money AmenitiesUpCharge { get; private set; }

    public Money TotalPrice { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTime Created { get; private set; }

    public DateTime? Confirmed { get; private set; }

    public DateTime? Rejected { get; private set; }

    public DateTime? Completed { get; private set; }

    public DateTime? Cancelled { get; private set; }

    public static Result<Booking> Reserve(
        Apartment apartment,
        Guid userId,
        DateRange duration,
        DateTime reserveDateTime,
        PricingService pricingService)
    {
        var pricingDetails = pricingService.CalculatePrice(apartment, duration);

        var bookingId = Guid.NewGuid();

        var booking = new Booking(
            bookingId,
            apartment.Id,
            userId,
            duration,
            pricingDetails.PriceForPeriod,
            pricingDetails.CleaningFee,
            pricingDetails.AmenitiesUpCharge,
            pricingDetails.TotalPrice,
            BookingStatus.Reserved,
            reserveDateTime);

        booking.RaiseDomainEvent(new BookingReservedDomainEvent(bookingId));

        apartment.LastBooked = reserveDateTime;

        return booking;
    }

    public Result Confirm(DateTime confirmedDateTime)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }

        Status = BookingStatus.Confirmed;

        Confirmed = confirmedDateTime;

        RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));

        return Result.Success();
    }

    public Result Reject(DateTime rejectedDateTime)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }

        Status = BookingStatus.Rejected;

        Rejected = rejectedDateTime;

        RaiseDomainEvent(new BookingRejectedDomainEvent(Id));

        return Result.Success();
    }

    public Result Complete(DateTime completedDateTime)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }

        Status = BookingStatus.Completed;

        Completed = completedDateTime;

        RaiseDomainEvent(new BookingCompletedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel(DateTime cancelledDateTime)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }

        var currentDate = DateOnly.FromDateTime(cancelledDateTime);

        if (currentDate > Duration.Start)
        {
            return Result.Failure(BookingErrors.AlreadyStarted);
        }

        Status = BookingStatus.Cancelled;

        Cancelled = cancelledDateTime;

        RaiseDomainEvent(new BookingCancelledDomainEvent(Id));

        return Result.Success();
    }
}