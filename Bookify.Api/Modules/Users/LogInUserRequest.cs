namespace Bookify.Api.Modules.Users;

public sealed record LogInUserRequest(
    string Email,
    string Password);