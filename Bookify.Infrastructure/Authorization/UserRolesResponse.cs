using Bookify.Domain.Users.Entity;
using System.Collections.Generic;
using System;

namespace Bookify.Infrastructure.Authorization;

public class UserRolesResponse
{
    public Guid UserId { get; init; }

    public List<Role> Roles { get; init; } = [];
}