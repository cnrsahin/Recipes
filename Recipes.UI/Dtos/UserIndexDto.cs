using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Dtos
{
    public class UserIndexDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Picture { get; set; }
        public string About { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }
}
