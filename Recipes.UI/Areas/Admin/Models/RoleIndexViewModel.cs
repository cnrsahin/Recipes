using Recipes.Service.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Areas.Admin.Models
{
    public class RoleIndexViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
    }
}
