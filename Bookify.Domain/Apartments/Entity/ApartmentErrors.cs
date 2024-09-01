using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments.Entity;

public static class ApartmentErrors
{
    public static readonly Error NotFound = new(
        "Apartment.NotFound",
        "The apartment with the specified identifier was not found");
}