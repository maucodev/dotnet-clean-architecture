using Bookify.Domain.Users.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Bookify.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken);
}