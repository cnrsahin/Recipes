﻿using Recipes.Service.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Areas.Admin.Models
{
    public class UserIndexViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}