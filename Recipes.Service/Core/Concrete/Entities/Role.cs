using Microsoft.AspNetCore.Identity;

namespace Recipes.Service.Core.Concrete.Entities
{
    public class Role : IdentityRole<int>
    {
        public string RoleDescription { get; set; }
    }
}
