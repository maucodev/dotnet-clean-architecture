using Bookify.Application.Users.GetLoggedInUser;
using Bookify.Application.Users.LoginUser;
using Bookify.Application.Users.RegisterUser;
using MediatR;

namespace Bookify.Api.Modules.Users;

public static class UsersModule
{
    public static void MapUsersEndpoints(this IEndpointRouteBuilder builder)
    {
        builder
            .MapGet(
                pattern: "users/me",
                handler: GetLoggedInUser)
            .RequireAuthorization();

        builder.MapPost(
            pattern: "users/register",
            handler: Register);

        builder.MapPost(
            pattern: "users/login",
            handler: LogIn);
    }

    private static async Task<IResult> GetLoggedInUser(ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetLoggedInUserQuery();

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Register(RegisterUserRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);

        var result = await sender.Send(command, cancellationToken);

        return result.IsFailure 
            ? Results.BadRequest(result.Error) 
            : Results.Ok(result.Value);
    }

    private static async Task<IResult> LogIn(LogInUserRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new LogInUserCommand(request.Email, request.Password);

        var result = await sender.Send(command, cancellationToken);

        return result.IsFailure 
            ? Results.Unauthorized() 
            : Results.Ok(result.Value);
    }
}