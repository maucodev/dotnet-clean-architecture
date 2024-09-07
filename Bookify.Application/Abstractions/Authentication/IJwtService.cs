using System.Threading;
using System.Threading.Tasks;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken);
}