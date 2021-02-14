using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Service.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes.Service.Data.Configurations
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Fullname).IsRequired().HasMaxLength(30);
            builder.Property(c => c.CommentText).IsRequired().HasMaxLength(500);
            builder.Property(c => c.EmailAddress).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CreateDate).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.IsConfirmed).IsRequired();
            builder.Property(c => c.WhyNotConfirmed).HasMaxLength(250);


            builder.HasOne<Recipe>(c => c.Recipe).WithMany(r => r.Comments).HasForeignKey(c => c.RecipeId)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne<User>(c => c.User).WithMany(r => r.Comments).HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.ToTable("Comments");

            builder.HasData(
                new Comment
                {
                    Id = 1,
                    RecipeId = 1,
                    UserId = 1,
                    EmailAddress = "cnrshn@gmail.com",
                    CommentText = "Tatlı çok güzel olmuş.",
                    Fullname = "Caner Sahin",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Comment
                {
                    Id = 2,
                    RecipeId = 2,
                    UserId = 2,
                    EmailAddress = "cnrshn@gmail.com",
                    CommentText = "Güzel bir börek olmuş",
                    Fullname = "Caner Sahin",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Comment
                {
                    Id = 3,
                    RecipeId = 3,
                    UserId = 1,
                    EmailAddress = "cnrshn@gmail.com",
                    CommentText = "Tatlıya bayıldım...",
                    Fullname = "Caner Sahin",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Comment
                {
                    Id = 4,
                    RecipeId = 4,
                    UserId = 2,
                    EmailAddress = "cnrshn@gmail.com",
                    CommentText = "Tatlıya bayıldım...",
                    Fullname = "Caner Sahin",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Comment
                {
                    Id = 5,
                    RecipeId = 5,
                    UserId = 2,
                    EmailAddress = "cnrshn@gmail.com",
                    CommentText = "Tatlıya bayıldım...",
                    Fullname = "Caner Sahin",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Comment
                {
                    Id = 6,
                    RecipeId = 6,
                    UserId = 1,
                    EmailAddress = "cnrshn@gmail.com",
                    CommentText = "Tatlıya bayıldım...",
                    Fullname = "Caner Sahin",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Comment
                {
                    Id = 7,
                    RecipeId = 7,
                    UserId = 2,
                    EmailAddress = "cnrshn@gmail.com",
                    CommentText = "Tatlıya bayıldım...",
                    Fullname = "Caner Sahin",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Comment
                {
                    Id = 8,
                    RecipeId = 8,
                    UserId = 2,
                    EmailAddress = "cnrshn@gmail.com",
                    CommentText = "Tatlıya bayıldım...",
                    Fullname = "Caner Sahin",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                });
        }
    }
}
