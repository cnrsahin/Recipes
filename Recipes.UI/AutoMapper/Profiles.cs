using AutoMapper;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<RecipeIndexDto, Recipe>().ReverseMap();
            CreateMap<FoodCategoryIndexDto, FoodCategory>().ReverseMap();
            CreateMap<CommentIndexDto, Comment>().ReverseMap();
            CreateMap<UserIndexDto, User>().ReverseMap();

        }
    }
}
