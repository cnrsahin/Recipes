using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recipes.Service.Core.Concrete.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string CommentText { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public bool IsConfirmed { get; set; } = false;
        public string WhyNotConfirmed { get; set; } = null;

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
