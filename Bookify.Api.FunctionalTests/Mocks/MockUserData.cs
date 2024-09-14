using Bookify.Api.Modules.Users;

namespace Bookify.Api.FunctionalTests.Mocks;

internal static class MockUserData
{
    public static RegisterUserRequest RegisterTestUserRequest = new("test@test.com", "test", "test", "12345");
}