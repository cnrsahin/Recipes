using Recipes.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Areas.Admin.Models
{
    public class FoodCategoryIndexViewModel
    {
        public IEnumerable<FoodCategoryIndexDto> FoodCategories { get; set; }
    }
}
