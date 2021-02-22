using AutoMapper;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.UI.Dtos;

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
            CreateMap<FoodCategory, FoodCategoryAddDto>().ReverseMap();
        }
    }
}
