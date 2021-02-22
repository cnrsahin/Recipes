using Recipes.UI.Dtos;
using System.Collections.Generic;

namespace Recipes.UI.Areas.Admin.Models
{
    public class RecipeIndexViewModel
    {
        public IEnumerable<RecipeIndexDto> Recipes { get; set; }
    }
}
