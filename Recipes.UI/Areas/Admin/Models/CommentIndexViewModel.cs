using Recipes.UI.Dtos;
using System.Collections.Generic;

namespace Recipes.UI.Areas.Admin.Models
{
    public class CommentIndexViewModel
    {
        public IEnumerable<CommentIndexDto> Comments { get; set; }
    }
}
