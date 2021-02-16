using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.Service.Core.Abstract;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Areas.Admin.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFoodCategoryRepository _foodCategoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public MenuViewComponent(UserManager<User> userManager, IRecipeRepository recipeRepository, IFoodCategoryRepository foodCategoryRepository, ICommentRepository commentRepository, RoleManager<Role> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
            _foodCategoryRepository = foodCategoryRepository ?? throw new ArgumentNullException(nameof(foodCategoryRepository));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null || roles == null)
                return Content("Kullanıcı veya Yetki Hatası");

            int deletedRecipeCount = await _recipeRepository.CountAsync(x => x.IsDeleted && x.IsConfirmed);
            int deletedFoodCategoryCount = await _foodCategoryRepository.CountAsync(x => x.IsDeleted && x.IsConfirmed);
            int deletedCommentCount = await _commentRepository.CountAsync(x => x.IsDeleted && x.IsConfirmed);

            int recipeCount = await _recipeRepository.CountAsync(c => !c.IsDeleted && c.IsConfirmed);
            int foodCategoryCount = await _foodCategoryRepository.CountAsync(c => !c.IsDeleted && c.IsConfirmed);
            int commentCount = await _commentRepository.CountAsync(c => !c.IsDeleted && c.IsConfirmed);

            int userCount = _userManager.Users.Count();
            int roleCount = _roleManager.Roles.Count();

            return View(new RightSideBarViewModel
            {
                Roles = roles,
                User = user,
                RecipeDeletedCount = deletedRecipeCount,
                FoodCategoryDeletedCount = deletedFoodCategoryCount,
                CommentDeletedCount = deletedCommentCount,

                RecipeCount = recipeCount,
                FoodCategoryCount = foodCategoryCount,
                CommentCount = commentCount,

                UserCount = userCount,
                RoleCount = roleCount
            });
        }
    }
}
