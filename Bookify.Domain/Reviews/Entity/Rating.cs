using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews.Entity;

public sealed record Rating
{
    public int Value { get; init; }

    public Comment Comment { get; init; }

    private Rating(int value, Comment comment)
    {
        Value = value;
        Comment = comment;
    }

    public static Result<Rating> Create(int value, Comment comment)
    {
        return value is < 1 or > 5
            ? Result.Failure<Rating>(RatingErrors.Invalid)
            : new Rating(value, comment);
    }
}