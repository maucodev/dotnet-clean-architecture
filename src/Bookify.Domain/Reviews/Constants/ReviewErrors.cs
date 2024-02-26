using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews.Constants;

public static class ReviewErrors
{
    public static readonly Error NotEligible = new(
        Code: "Review.NotEligible",
        Description: "The review is not eligible because the booking is not yet completed");
}