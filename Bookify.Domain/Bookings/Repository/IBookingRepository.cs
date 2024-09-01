using System;
using System.Threading;
using System.Threading.Tasks;
using Bookify.Domain.Apartments.Entity;
using Bookify.Domain.Bookings.Entity;

namespace Bookify.Domain.Bookings.Repository;

public interface IBookingRepository
{
    void Add(Booking booking);

    Task<Booking> GetByIdAsync(Guid notificationBookingId, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken = default);
}