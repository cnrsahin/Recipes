using Recipes.Service.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes.Service.Core.Abstract
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
    }
}
