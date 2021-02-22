using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Recipes.Service.Core.Concrete.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string FoodPicture { get; set; }
        public string FoodTitle { get; set; }
        public int PersonSize { get; set; } = 4;
        public int ViewCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public string SeoAuthor { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTags { get; set; }
        public TimeSpan PreparationTime { get; set; }
        public TimeSpan CookingTime { get; set; }
        public string Material { get; set; }
        public string RecipeDetail { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public int FoodCategoryId { get; set; }
        public FoodCategory FoodCategory { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsConfirmed { get; set; } = false;
        public string WhyNotConfirmed { get; set; } = null;


        public ICollection<Comment> Comments { get; set; }

        public Recipe()
        {
            Comments = new Collection<Comment>();
        }
    }
}
