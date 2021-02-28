using Recipes.Service.Results.Abstract;
using Recipes.UI.Dtos;
using System.Collections.Generic;

namespace Recipes.UI.Areas.Admin.Models
{
    public class FoodCategoryIndexViewModel
    {
        public IDataResult<IEnumerable<FoodCategoryIndexDto>> FoodCategoriesResult { get; set; }
    }
}
