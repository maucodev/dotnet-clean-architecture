using System.Collections.Generic;
using System;
using Bookify.Domain.Roles.Entity;

namespace Bookify.Infrastructure.Authorization;

public class UserRolesResponse
{
    public Guid UserId { get; init; }

    public List<Role> Roles { get; init; } = [];
}