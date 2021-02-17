using Recipes.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Areas.Admin.Models
{
    public class RecipeIndexViewModel
    {
        public IEnumerable<RecipeIndexDto> Recipes { get; set; }
    }
}
