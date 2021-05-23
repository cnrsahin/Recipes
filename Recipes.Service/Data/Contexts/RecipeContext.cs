using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.Service.Data.Configurations;
using System;

namespace Recipes.Service.Data.Contexts
{
    public class RecipeContext : IdentityDbContext<User, Role, int, UserClaim,
        UserRole, UserLogin, RoleClaim, UserToken>
    {
        public virtual DbSet<FoodCategory> FoodCategories { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FoodCategoryMap());
            builder.ApplyConfiguration(new RecipeMap());
            builder.ApplyConfiguration(new CommentMap());
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new UserClaimMap());
            builder.ApplyConfiguration(new UserLoginMap());
            builder.ApplyConfiguration(new UserTokenMap());
            builder.ApplyConfiguration(new RoleClaimMap());
            builder.ApplyConfiguration(new UserRoleMap());
        }
    }
}
