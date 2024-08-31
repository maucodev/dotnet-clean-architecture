using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews.Entity;

public static class RatingErrors
{
    public static readonly Error Invalid = new(
        "Rating.Invalid",
        "The rating is invalid");
}