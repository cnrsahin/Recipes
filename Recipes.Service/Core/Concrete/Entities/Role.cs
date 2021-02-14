using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes.Service.Core.Concrete.Entities
{
    public class Role : IdentityRole<int>
    {
        public string RoleDescription { get; set; }
    }
}
