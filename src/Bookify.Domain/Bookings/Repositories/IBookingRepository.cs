using Bookify.Domain.Apartments.Entities;
using Bookify.Domain.Bookings.Entities;

namespace Bookify.Domain.Bookings.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken);

    void Add(Booking booking);
}