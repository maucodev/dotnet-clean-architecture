using System.Collections.Generic;
using Bookify.Domain.Users.Entity;
using System.Linq;
using System.Threading.Tasks;
using Bookify.Domain.Roles.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;

    public AuthorizationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        var roles = await _dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserRolesResponse
            {
                UserId = u.Id,
                Roles = u.Roles.ToList()
            })
            .FirstAsync();

        return roles;
    }

    public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
    {
        var userRolesResponse = await GetRolesForUserAsync(identityId);

        if (userRolesResponse is null || userRolesResponse.Roles.Count == 0)
        {
            return [];
        }

        var result = new HashSet<string>();

        foreach (var role in userRolesResponse.Roles)
        {
            var permissions = await _dbContext.Set<RolePermission>()
                .Where(rp => rp.RoleId == role.Id)
                .Select(rp => rp.Permission.Name)
                .ToListAsync();

            foreach (var permission in permissions)
            {
                result.Add(permission);
            }
        }

        return result;
    }
}