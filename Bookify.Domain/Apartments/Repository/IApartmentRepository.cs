using System;
using System.Threading;
using System.Threading.Tasks;
using Bookify.Domain.Apartments.Entity;

namespace Bookify.Domain.Apartments.Repository;

public interface IApartmentRepository
{
    Task<Apartment> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}