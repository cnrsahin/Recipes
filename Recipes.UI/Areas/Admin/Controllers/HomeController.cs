using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.Service.Core.Abstract;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.UI.Areas.Admin.Models;
using System;
using System.Threading.Tasks;

namespace Recipes.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFoodCategoryRepository _foodCategoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;

        public HomeController(IRecipeRepository recipeRepository, IFoodCategoryRepository foodCategoryRepository = null,
            ICommentRepository commentRepository = null, UserManager<User> userManager = null)
        {
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
            _foodCategoryRepository = foodCategoryRepository ?? throw new ArgumentNullException(nameof(foodCategoryRepository));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [Authorize(Roles = "Admin, Editor, Member")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int waitConfirmRecipeCount = await _recipeRepository.CountAsync(r => !r.IsConfirmed && !r.IsDeleted);
            int waitConfirmFoodCategoryCount = await _foodCategoryRepository.CountAsync(f => !f.IsConfirmed && !f.IsDeleted);
            int waitConfirmCommentCount = await _commentRepository.CountAsync(c => !c.IsConfirmed && !c.IsDeleted);

            int notConfirmedAndDeletedRecipeCount = await _recipeRepository.CountAsync(r => r.IsDeleted && !r.IsConfirmed);
            int notConfirmedAndDeletedFoodCategoryCount = await _foodCategoryRepository.CountAsync(f => f.IsDeleted && !f.IsConfirmed);
            int notConfirmedAndDeletedCommentCount = await _commentRepository.CountAsync(c => c.IsDeleted && !c.IsConfirmed);

            var model = new HomeIndexViewModel
            {
                WaitConfirmRecipeCount = waitConfirmRecipeCount,
                WaitConfirmFoodCategoryCount = waitConfirmFoodCategoryCount,
                WaitConfirmCommentCount = waitConfirmCommentCount,

                NotConfirmedAndDeletedRecipeCount = notConfirmedAndDeletedRecipeCount,
                NotConfirmedAndDeletedFoodCategoryCount = notConfirmedAndDeletedFoodCategoryCount,
                NotConfirmedAndDeletedCommentCount = notConfirmedAndDeletedCommentCount
            };

            return View(model);
        }
    }
}

