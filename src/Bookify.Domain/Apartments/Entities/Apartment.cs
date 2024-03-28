using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments.Constants;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Apartments.Entities;

public sealed class Apartment : Entity
{
    public Apartment(
        Guid id,
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        List<Amenity> amenities)
    : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
        Amenities = amenities;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    // ReSharper disable once UnusedMember.Local
    private Apartment()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Name? Name { get; private set; }

    public Description? Description { get; private set; }

    public Address? Address { get; private set; }

    public Money Price { get; private set; }

    public Money CleaningFee { get; private set; }

    public DateTime? LastBookedOnUtc { get; internal set; }

    public List<Amenity> Amenities { get; private set; }
}