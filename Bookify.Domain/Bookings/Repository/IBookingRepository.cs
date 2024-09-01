using System.Threading;
using System.Threading.Tasks;
using Bookify.Domain.Apartments.Entity;
using Bookify.Domain.Bookings.Entity;

namespace Bookify.Domain.Bookings.Repository;

public interface IBookingRepository
{
    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken = default);

    void Add(Booking booking);
}