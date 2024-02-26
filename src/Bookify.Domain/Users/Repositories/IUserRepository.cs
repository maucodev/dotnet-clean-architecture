using Bookify.Domain.Users.Entities;

namespace Bookify.Domain.Users.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    void Add(User user);
}