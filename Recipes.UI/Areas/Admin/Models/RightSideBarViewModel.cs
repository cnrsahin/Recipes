using Recipes.Service.Core.Concrete.Entities;
using System.Collections.Generic;

namespace Recipes.UI.Areas.Admin.Models
{
    public class RightSideBarViewModel
    {
        public User User { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public int RecipeDeletedCount { get; set; }
        public int FoodCategoryDeletedCount { get; set; }
        public int CommentDeletedCount { get; set; }

        public int RecipeCount { get; set; }
        public int FoodCategoryCount { get; set; }
        public int CommentCount { get; set; }

        public int UserCount { get; set; }
        public int RoleCount { get; set; }
    }
}
