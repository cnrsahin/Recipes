using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Recipes.Service.Core.Concrete.Entities
{
    public class User : IdentityUser<int>
    {
        public string Picture { get; set; }
        public string About { get; set; }
        public string Fullname { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<FoodCategory> FoodCategories { get; set; }

        public User()
        {
            Recipes = new Collection<Recipe>();
            Comments = new Collection<Comment>();
            FoodCategories = new Collection<FoodCategory>();
        }
    }
}
