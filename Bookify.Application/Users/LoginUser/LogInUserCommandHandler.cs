using System.Threading;
using System.Threading.Tasks;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Entity;

namespace Bookify.Application.Users.LoginUser;

internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IJwtService _jwtService;

    public LogInUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);

        return result.IsFailure 
            ? Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials) 
            : new AccessTokenResponse(result.Value);
    }
}