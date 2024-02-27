using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Bookings.Repositories;
using Bookify.Domain.Users.Repositories;
using MediatR;

namespace Bookify.Application.Bookings.DomainEvents;

internal sealed class BookingReservedDomainEventHandler(
    IBookingRepository bookingRepository,
    IUserRepository userRepository,
    IEmailService emailService)
    : INotificationHandler<BookingReservedDomainEvent>
{
    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

        if (booking is null)
        {
            return;
        }

        var user = await userRepository.GetByIdAsync(booking.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        await emailService.SendAsync(
            user.Email,
            new Subject("Booking reserved!"),
            new Body("You have 10 minutes to confirm this booking"));
    }
}