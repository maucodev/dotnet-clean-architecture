using Bookify.Domain.Apartments.Entities;

namespace Bookify.Domain.Apartments.Repositories;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}