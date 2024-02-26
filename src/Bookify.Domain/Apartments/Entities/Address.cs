namespace Bookify.Domain.Apartments.Entities;

public record Address(
    string Country,
    string State,
    string ZipCode,
    string City,
    string Street);