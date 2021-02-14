using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes.Service.Core.Abstract;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Recipes.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IFoodCategoryRepository _foodCategoryRepository;

        public UserController(UserManager<User> userManager, IRecipeRepository recipeRepository, ICommentRepository commentRepository, IFoodCategoryRepository foodCategoryRepository)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _recipeRepository = recipeRepository;
            _commentRepository = commentRepository;
            _foodCategoryRepository = foodCategoryRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null)
                return NotFound();
            else
            {
                var model = new UserIndexViewModel
                {
                    Users = users
                };

                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> DoActiveOrPassive(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.IsActive = user.IsActive ? false : true;

            var updatedUser = await _userManager.UpdateAsync(user);
            var result = JsonSerializer.Serialize(updatedUser);

            return Json(result);
        }
    }
}
