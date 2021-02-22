using Recipes.UI.Dtos;
using System.Collections.Generic;

namespace Recipes.UI.Areas.Admin.Models
{
    public class FoodCategoryIndexViewModel
    {
        public IEnumerable<FoodCategoryIndexDto> FoodCategories { get; set; }
    }
}
