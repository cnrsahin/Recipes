﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Service.Core.Concrete.Entities;
using System;

namespace Recipes.Service.Data.Configurations
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(25);
            builder.Property(u => u.NormalizedName).HasMaxLength(25);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            builder.Property(r => r.RoleDescription).IsRequired().HasMaxLength(250);

            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    RoleDescription = "Tüm yetkilere sahip kullanıcıdır."
                },
                new Role
                {
                    Id = 2,
                    Name = "Editor",
                    NormalizedName = "EDITOR",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    RoleDescription = "Tarif ve kategori ekleyebilir fakat silemez, Kullanıcılar ve roller sayfalarını görüntüleyemez."
                },
                new Role
                {
                    Id = 3,
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    RoleDescription = "Üye olan herkese atanır, tarif yazabilirler ve yorum bırakabilirler, admin veya editor tarafından onaylanması gerekir." +
                    " Kullanıcılar, roller ve çöp kutusu sayfalarını görüntüleyemez."
                });
        }
    }
}
