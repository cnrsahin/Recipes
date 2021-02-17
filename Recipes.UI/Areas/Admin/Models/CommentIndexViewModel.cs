using Recipes.Service.Core.Concrete.Entities;
using Recipes.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Areas.Admin.Models
{
    public class CommentIndexViewModel
    {
        public IEnumerable<CommentIndexDto> Comments { get; set; }
    }
}
