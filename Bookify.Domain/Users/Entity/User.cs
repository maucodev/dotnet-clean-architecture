using System;
using System.Collections.Generic;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users.Entity;

public sealed class User : Abstractions.Entity
{
    private readonly List<Role> _roles = [];

    private User()
    {
    }

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

    public string IdentityId { get; private set; } = string.Empty;

    public IReadOnlyCollection<Role> Roles => _roles;

    public static Result<User> Create(FirstName firstName, LastName lastName, Email email)
    {
        var userId = Guid.NewGuid();

        var user = new User(userId, firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(userId));

        user._roles.Add(Role.Registered);

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}