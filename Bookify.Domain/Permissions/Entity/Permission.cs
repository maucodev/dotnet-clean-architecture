namespace Bookify.Domain.Permissions.Entity;

public sealed class Permission
{
    public static readonly Permission ApartmentsRead = new(1, "apartments:read");

    private Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; }
}