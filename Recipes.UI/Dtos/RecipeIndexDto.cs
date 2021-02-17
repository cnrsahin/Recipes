using Recipes.Service.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Dtos
{
    public class RecipeIndexDto
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string FoodPicture { get; set; }
        public string FoodTitle { get; set; }
        public string FoodCategoryName { get; set; }
        public string UserName { get; set; }
        public string WhyNotConfirmed { get; set; }
    }
}
