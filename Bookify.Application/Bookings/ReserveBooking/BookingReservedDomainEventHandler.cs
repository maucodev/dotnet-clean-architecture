﻿using System.Threading;
using System.Threading.Tasks;
using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Bookings.Repository;
using Bookify.Domain.Users.Repository;
using MediatR;

namespace Bookify.Application.Bookings.ReserveBooking;

internal sealed class BookingReservedDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public BookingReservedDomainEventHandler(
        IBookingRepository bookingRepository,
        IUserRepository userRepository)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

        if (booking == null)
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(booking.UserId, cancellationToken);

        if (user == null)
        {
            return;
        }

        const string subject = "Booking reserved!";
        const string body = "You have 10 minutes to confirm this booking.";

        await _emailService.SendAsync(user.Email, subject, body);
    }
}