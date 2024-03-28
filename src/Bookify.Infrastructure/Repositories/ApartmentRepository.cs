using Bookify.Domain.Apartments.Entities;
using Bookify.Domain.Apartments.Repositories;

namespace Bookify.Infrastructure.Repositories;

internal sealed class ApartmentRepository(ApplicationDbContext dbContext) 
    : Repository<Apartment>(dbContext), IApartmentRepository;