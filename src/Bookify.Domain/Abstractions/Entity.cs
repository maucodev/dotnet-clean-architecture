namespace Bookify.Domain.Abstractions;

public abstract class Entity(Guid id)
{
    public Guid Id { get; init; } = id;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (Entity)obj;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}