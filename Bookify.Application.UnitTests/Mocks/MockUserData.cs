using Bookify.Domain.Users.Entity;

namespace Bookify.Application.UnitTests.Mocks;

internal static class MockUserData
{
    public static User Create() => User.Create(FirstName, LastName, Email).Value;

    public static readonly FirstName FirstName = new("First");
    public static readonly LastName LastName = new("Last");
    public static readonly Email Email = new("test@test.com");
}
