﻿using System.Collections.Generic;

namespace Bookify.Domain.Users.Entity;

public sealed class Role
{
    public static readonly Role Registered = new(1, "Registered");

    private Role()
    {
    }

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public ICollection<User> Users { get; init; } = new List<User>();

    public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
}