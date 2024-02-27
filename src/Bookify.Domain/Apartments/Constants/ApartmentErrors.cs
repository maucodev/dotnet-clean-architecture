using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments.Constants;

public static class ApartmentErrors
{
    public static readonly Error NotFound = new(
        Code: "Apartment.NotFound",
        Description: "The apartment with the specified identifier was not found");
}