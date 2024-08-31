﻿using System;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users.Entity;

public sealed class User : Abstractions.Entity
{
    private User(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }

    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        var userId = Guid.NewGuid();

        var user = new User(userId, firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(userId));

        return user;
    }
}