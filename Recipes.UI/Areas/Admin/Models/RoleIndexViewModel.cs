using Recipes.Service.Core.Concrete.Entities;
using System.Collections.Generic;

namespace Recipes.UI.Areas.Admin.Models
{
    public class RoleIndexViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
    }
}
