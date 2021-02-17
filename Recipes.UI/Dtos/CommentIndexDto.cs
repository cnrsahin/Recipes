using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Dtos
{
    public class CommentIndexDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string CommentText { get; set; }
        public string UserName { get; set; }
        public string RecipeName { get; set; }
        public string WhyNotConfirmed { get; set; }
    }
}
