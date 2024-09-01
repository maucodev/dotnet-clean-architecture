using System.Threading.Tasks;
using Bookify.Application.Abstractions.Email;

namespace Bookify.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Domain.Users.Entity.Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}