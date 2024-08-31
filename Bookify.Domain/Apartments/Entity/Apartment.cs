using System;
using System.Collections.Generic;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Apartments.Entity;

public sealed class Apartment : Abstractions.Entity
{
    private Apartment(
        Guid id,
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        List<Amenity> amenities) : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
        Amenities = amenities;
    }

    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public Address Address { get; private set; }

    public Money Price { get; private set; }

    public Money CleaningFee { get; private set; }

    public DateTime? LastBooked { get; internal set; }

    public List<Amenity> Amenities { get; private set; }

    public static Result<Apartment> Create(
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        List<Amenity> amenities)
    {
        var apartmentId = Guid.NewGuid();

        var apartment = new Apartment(
            apartmentId,
            name,
            description,
            address,
            price,
            cleaningFee,
            amenities);

        return apartment;
    }
}