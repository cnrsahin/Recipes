﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Recipes.Service.Core.Concrete.Entities
{
    public class FoodCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPicture { get; set; }
        public string CategoryDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsConfirmed { get; set; }
        public string WhyNotConfirmed { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Recipe> Recipes { get; set; }

        public FoodCategory()
        {
            Recipes = new Collection<Recipe>();
        }
    }
}
