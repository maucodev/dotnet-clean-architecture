using System;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users.Entity;

public sealed class User : Abstractions.Entity
{
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

    public static Result<User> Create(FirstName firstName, LastName lastName, Email email)
    {
        var userId = Guid.NewGuid();

        var user = new User(userId, firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(userId));

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}