using Bookify.Domain.Users.Entities;
using Bookify.Domain.Users.Repositories;

namespace Bookify.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) 
    : Repository<User>(dbContext), IUserRepository;