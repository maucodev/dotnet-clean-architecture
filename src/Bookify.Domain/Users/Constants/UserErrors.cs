using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users.Constants;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        Code: "User.NotFound",
        Description: "The user with the specified identifier was not found");

    public static readonly Error InvalidCredentials = new(
        Code: "User.InvalidCredentials",
        Description: "The provided credentials were invalid");
}