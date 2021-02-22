using Recipes.Service.Core.Abstract;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.Service.Data.Contexts;

namespace Recipes.Service.Data.EntityFramework
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RecipeContext context) : base(context)
        {

        }
    }
}
