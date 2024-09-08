using System.Collections.Generic;
using Bookify.Domain.Permissions.Entity;
using Bookify.Domain.Users.Entity;

namespace Bookify.Domain.Roles.Entity;

public sealed class Role
{
    public static readonly Role Registered = new(1, "Registered");

    private Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; }

    public ICollection<User> Users { get; init; } = new List<User>();

    public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
}