using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Service.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes.Service.Data.Configurations
{
    public class FoodCategoryMap : IEntityTypeConfiguration<FoodCategory>
    {
        public void Configure(EntityTypeBuilder<FoodCategory> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).UseIdentityColumn();
            builder.Property(f => f.CategoryName).IsRequired().HasMaxLength(20);
            builder.Property(f => f.CategoryPicture).IsRequired().HasMaxLength(250);
            builder.Property(f => f.CategoryDescription).IsRequired().HasMaxLength(150);
            builder.Property(f => f.CreateDate).IsRequired();
            builder.Property(f => f.IsDeleted).IsRequired();
            builder.Property(f => f.IsConfirmed).IsRequired();
            builder.Property(f=> f.WhyNotConfirmed).HasMaxLength(250);


            builder.HasOne<User>(c => c.User).WithMany(r => r.FoodCategories).HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.ToTable("FoodCategories");

            builder.HasData(
                new FoodCategory
                {
                    Id = 1,
                    UserId = 1,
                    CategoryName = "Tatlılar",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Tatlı Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new FoodCategory
                {
                    Id = 2,
                    UserId = 1,
                    CategoryName = "Börekler",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Börek Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new FoodCategory
                {
                    Id = 3,
                    UserId = 1,
                    CategoryName = "Çorbalar",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Çorba Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new FoodCategory
                {
                    Id = 4,
                    UserId = 1,
                    CategoryName = "Kekler",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Kek Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new FoodCategory
                {
                    Id = 5,
                    UserId = 1,
                    CategoryName = "Makarnalar",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Makarna Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new FoodCategory
                {
                    Id = 6,
                    UserId = 2,
                    CategoryName = "Pastalar",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Pasta Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new FoodCategory
                {
                    Id = 7,
                    UserId = 2,
                    CategoryName = "Kahvaltılıklar",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Kahvaltı Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new FoodCategory
                {
                    Id = 8,
                    UserId = 2,
                    CategoryName = "Kurabiyeler",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Kurabiye Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new FoodCategory
                {
                    Id = 9,
                    UserId = 2,
                    CategoryName = "Salatalar",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Salata Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new FoodCategory
                {
                    Id = 10,
                    UserId = 2,
                    CategoryName = "Sulu Yemekler",
                    CategoryPicture = "default.jpg",
                    CategoryDescription = "Sulu Yemek Kategorisi",
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                });
        }
    }
}
