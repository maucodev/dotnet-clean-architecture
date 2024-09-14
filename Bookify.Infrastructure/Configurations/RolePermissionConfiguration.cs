﻿using Bookify.Domain.Permissions.Entity;
using Bookify.Domain.Roles.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasKey(rolePermission => new { rolePermission.RoleId, rolePermission.PermissionId });

        builder.HasData(new RolePermission
        {
            RoleId = Role.Registered.Id,
            PermissionId = Permission.ApartmentsRead.Id
        });
    }
}