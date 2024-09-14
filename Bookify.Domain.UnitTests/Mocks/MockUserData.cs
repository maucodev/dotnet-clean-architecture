using Bookify.Domain.Users.Entity;

namespace Bookify.Domain.UnitTests.Mocks;

internal static class MockUserData
{
    public static readonly FirstName FirstName = new("First");
    public static readonly LastName LastName = new("Last");
    public static readonly Email Email = new("test@bookify.com");
}