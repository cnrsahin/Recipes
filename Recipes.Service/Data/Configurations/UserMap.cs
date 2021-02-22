using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Service.Core.Concrete.Entities;
using System;

namespace Recipes.Service.Data.Configurations
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(20);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(20);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(100);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();


            builder.Property(u => u.Picture).IsRequired().HasMaxLength(250);
            builder.Property(u => u.About).HasMaxLength(500);
            builder.Property(u => u.Fullname).HasMaxLength(30);

            var admin = new User
            {
                Id = 1,
                Fullname = "Caner Sahin",
                About = "1 id'li kullanıcı",
                Picture = "default.jpg",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "00905425612223",
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                IsActive = true
            };
            admin.PasswordHash = CreatePasswordHash(admin, "12345");

            var editor = new User
            {
                Id = 2,
                Fullname = "Caner Sahin",
                About = "2 id'li kullanıcı",
                Picture = "default.jpg",
                Email = "editor@gmail.com",
                NormalizedEmail = "EDITOR@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "00905425612223",
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                UserName = "Editor",
                NormalizedUserName = "EDITOR",
                IsActive = true
            };
            editor.PasswordHash = CreatePasswordHash(editor, "12345");

            builder.HasData(admin, editor);
        }

        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();

            return passwordHasher.HashPassword(user, password);
        }
    }
}
