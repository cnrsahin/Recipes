using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Dtos
{
    public class FoodCategoryIndexDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPicture { get; set; }
        public string CategoryDescription { get; set; }
        public string UserName { get; set; }
        public string WhyNotConfirmed { get; set; }
    }
}
