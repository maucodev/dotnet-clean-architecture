using System;
using System.Collections.Generic;
using Bookify.Domain.Users.Entity;
using System.Linq;
using System.Threading.Tasks;
using Bookify.Application.Caching;
using Bookify.Domain.Roles.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        var cacheKey = $"auth:get-roles-for-user:{identityId}";
        var cachedData = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

        if (cachedData is not null)
        {
            return cachedData;
        }

        var result = await _dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserRolesResponse
            {
                UserId = u.Id,
                Roles = u.Roles.ToList()
            })
            .FirstAsync();

        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromSeconds(30));

        return result;
    }

    public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
    {
        var userRolesResponse = await GetRolesForUserAsync(identityId);

        if (userRolesResponse is null || userRolesResponse.Roles.Count == 0)
        {
            return [];
        }

        var cacheKey = $"auth:get-permissions-for-user:{identityId}";
        var cachedData = await _cacheService.GetAsync<HashSet<string>>(cacheKey);

        if (cachedData is not null)
        {
            return cachedData;
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

        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromSeconds(30));

        return result;
    }
}