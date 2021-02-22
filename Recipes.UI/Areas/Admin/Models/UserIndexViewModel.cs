using Recipes.UI.Dtos;
using System.Collections.Generic;

namespace Recipes.UI.Areas.Admin.Models
{
    public class UserIndexViewModel
    {
        public IEnumerable<UserIndexDto> Users { get; set; }
    }
}
