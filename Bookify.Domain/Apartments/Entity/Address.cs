﻿namespace Bookify.Domain.Apartments.Entity;

public record Address(
    string Country,
    string State,
    string ZipCode,
    string City,
    string Street);