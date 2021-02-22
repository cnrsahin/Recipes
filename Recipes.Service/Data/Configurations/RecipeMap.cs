using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Service.Core.Concrete.Entities;
using System;

namespace Recipes.Service.Data.Configurations
{
    public class RecipeMap : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).UseIdentityColumn();
            builder.Property(r => r.FoodName).IsRequired().HasMaxLength(20);
            builder.Property(r => r.FoodPicture).IsRequired().HasMaxLength(250);
            builder.Property(r => r.FoodTitle).IsRequired().HasMaxLength(500);
            builder.Property(r => r.PersonSize).IsRequired();
            builder.Property(r => r.ViewCount).IsRequired();
            builder.Property(r => r.CommentCount).IsRequired();
            builder.Property(r => r.SeoAuthor).IsRequired().HasMaxLength(20);
            builder.Property(r => r.SeoDescription).IsRequired().HasMaxLength(180);
            builder.Property(r => r.SeoTags).IsRequired().HasMaxLength(200);


            builder.Property(r => r.PreparationTime).IsRequired();
            builder.Property(r => r.CookingTime).IsRequired();
            builder.Property(r => r.Material).IsRequired().HasMaxLength(500);
            builder.Property(r => r.RecipeDetail).IsRequired().HasMaxLength(500);
            builder.Property(r => r.CreateTime).IsRequired();
            builder.Property(r => r.IsDeleted).IsRequired();
            builder.Property(r => r.IsConfirmed).IsRequired();
            builder.Property(r => r.WhyNotConfirmed).HasMaxLength(250);


            builder.HasOne<FoodCategory>(r => r.FoodCategory).WithMany(c => c.Recipes).HasForeignKey(r => r.FoodCategoryId)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne<User>(r => r.User).WithMany(u => u.Recipes).HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.ToTable("Recipes");

            builder.HasData(
                new Recipe
                {
                    Id = 1,
                    UserId = 1,
                    FoodCategoryId = 1,
                    FoodName = "Kadayif Tatlısı",
                    CommentCount = 0,
                    ViewCount = 400,
                    SeoAuthor = "Caner Sahin",
                    SeoTags = "birinci, yemek, tarifi",
                    SeoDescription = "Kadayif tatlisi tarifi.",
                    CookingTime = TimeSpan.FromMinutes(15),
                    CreateTime = DateTime.Now,
                    FoodPicture = "default.jpg",
                    FoodTitle = "guzel tatli",
                    PersonSize = 4,
                    Material = "kadayıf tarifi malzemeler listesi, html text editor ile yazılacak.",
                    PreparationTime = TimeSpan.FromMinutes(25),
                    RecipeDetail = "15 dk pişir, soğut, servis et vs.",
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Recipe
                {
                    Id = 2,
                    UserId = 1,
                    FoodCategoryId = 2,
                    FoodName = "Avcı Böreği",
                    CommentCount = 0,
                    ViewCount = 299,
                    SeoAuthor = "Caner Sahin",
                    SeoTags = "börek, avcı, tarif",
                    SeoDescription = "Avcı böreği tarifi.",
                    CookingTime = TimeSpan.FromMinutes(15),
                    CreateTime = DateTime.Now,
                    FoodPicture = "default.jpg",
                    FoodTitle = "Tatlı tarifinin kısa bir başlığı.",
                    PersonSize = 4,
                    Material = "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.",
                    PreparationTime = TimeSpan.FromMinutes(25),
                    RecipeDetail = "15 dk pişir, soğut, servis et vs.",
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Recipe
                {
                    Id = 3,
                    UserId = 2,
                    FoodCategoryId = 3,
                    FoodName = "Ispanak Yemeği",
                    CommentCount = 0,
                    ViewCount = 299,
                    SeoAuthor = "Caner Sahin",
                    SeoTags = "börek, avcı, tarif",
                    SeoDescription = "Avcı böreği tarifi.",
                    CookingTime = TimeSpan.FromMinutes(15),
                    CreateTime = DateTime.Now,
                    FoodPicture = "default.jpg",
                    FoodTitle = "Tatlı tarifinin kısa bir başlığı.",
                    PersonSize = 4,
                    Material = "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.",
                    PreparationTime = TimeSpan.FromMinutes(25),
                    RecipeDetail = "15 dk pişir, soğut, servis et vs.",
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Recipe
                {
                    Id = 4,
                    UserId = 2,
                    FoodCategoryId = 4,
                    FoodName = "Domates Yemeği",
                    CommentCount = 0,
                    ViewCount = 299,
                    SeoAuthor = "Caner Sahin",
                    SeoTags = "börek, avcı, tarif",
                    SeoDescription = "Avcı böreği tarifi.",
                    CookingTime = TimeSpan.FromMinutes(15),
                    CreateTime = DateTime.Now,
                    FoodPicture = "default.jpg",
                    FoodTitle = "Tatlı tarifinin kısa bir başlığı.",
                    PersonSize = 4,
                    Material = "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.",
                    PreparationTime = TimeSpan.FromMinutes(25),
                    RecipeDetail = "15 dk pişir, soğut, servis et vs.",
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Recipe
                {
                    Id = 5,
                    UserId = 1,
                    FoodCategoryId = 5,
                    FoodName = "Patates Yemeği",
                    CommentCount = 0,
                    ViewCount = 299,
                    SeoAuthor = "Caner Sahin",
                    SeoTags = "börek, avcı, tarif",
                    SeoDescription = "Avcı böreği tarifi.",
                    CookingTime = TimeSpan.FromMinutes(15),
                    CreateTime = DateTime.Now,
                    FoodPicture = "default.jpg",
                    FoodTitle = "Tatlı tarifinin kısa bir başlığı.",
                    PersonSize = 4,
                    Material = "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.",
                    PreparationTime = TimeSpan.FromMinutes(25),
                    RecipeDetail = "15 dk pişir, soğut, servis et vs.",
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Recipe
                {
                    Id = 6,
                    UserId = 1,
                    FoodCategoryId = 6,
                    FoodName = "Soğan Yemeği",
                    CommentCount = 0,
                    ViewCount = 299,
                    SeoAuthor = "Caner Sahin",
                    SeoTags = "börek, avcı, tarif",
                    SeoDescription = "Avcı böreği tarifi.",
                    CookingTime = TimeSpan.FromMinutes(15),
                    CreateTime = DateTime.Now,
                    FoodPicture = "default.jpg",
                    FoodTitle = "Tatlı tarifinin kısa bir başlığı.",
                    PersonSize = 4,
                    Material = "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.",
                    PreparationTime = TimeSpan.FromMinutes(25),
                    RecipeDetail = "15 dk pişir, soğut, servis et vs.",
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Recipe
                {
                    Id = 7,
                    UserId = 2,
                    FoodCategoryId = 7,
                    FoodName = "Böğürtlen Yemeği",
                    CommentCount = 0,
                    ViewCount = 299,
                    SeoAuthor = "Caner Sahin",
                    SeoTags = "börek, avcı, tarif",
                    SeoDescription = "Avcı böreği tarifi.",
                    CookingTime = TimeSpan.FromMinutes(15),
                    CreateTime = DateTime.Now,
                    FoodPicture = "default.jpg",
                    FoodTitle = "Tatlı tarifinin kısa bir başlığı.",
                    PersonSize = 4,
                    Material = "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.",
                    PreparationTime = TimeSpan.FromMinutes(25),
                    RecipeDetail = "15 dk pişir, soğut, servis et vs.",
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                },
                new Recipe
                {
                    Id = 8,
                    UserId = 1,
                    FoodCategoryId = 8,
                    FoodName = "Peynir Yemeği",
                    CommentCount = 0,
                    ViewCount = 299,
                    SeoAuthor = "Caner Sahin",
                    SeoTags = "börek, avcı, tarif",
                    SeoDescription = "Avcı böreği tarifi.",
                    CookingTime = TimeSpan.FromMinutes(15),
                    CreateTime = DateTime.Now,
                    FoodPicture = "default.jpg",
                    FoodTitle = "Tatlı tarifinin kısa bir başlığı.",
                    PersonSize = 4,
                    Material = "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.",
                    PreparationTime = TimeSpan.FromMinutes(25),
                    RecipeDetail = "15 dk pişir, soğut, servis et vs.",
                    IsDeleted = false,
                    IsConfirmed = true,
                    WhyNotConfirmed = null
                });
        }
    }
}
