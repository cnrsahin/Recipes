using Recipes.Service.Core.Abstract;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.Service.Data.Contexts;

namespace Recipes.Service.Data.EntityFramework
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(RecipeContext context) : base(context)
        {
        }
    }
}
