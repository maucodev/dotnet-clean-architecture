using Bookify.Application.Abstractions.Email;

namespace Bookify.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Domain.Users.Entities.Email email, Subject subject, Body body)
    {
        return Task.CompletedTask;
    }
}