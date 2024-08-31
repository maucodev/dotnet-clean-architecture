using System;
using System.Threading;
using System.Threading.Tasks;
using Bookify.Domain.Users.Entity;

namespace Bookify.Domain.Users.Repository;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(User user);
}