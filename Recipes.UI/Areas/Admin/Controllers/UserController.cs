using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes.Service.Core.Abstract;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.UI.Areas.Admin.Models;
using Recipes.UI.Dtos;
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
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IRecipeRepository recipeRepository, ICommentRepository commentRepository, IFoodCategoryRepository foodCategoryRepository, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _foodCategoryRepository = foodCategoryRepository ?? throw new ArgumentNullException(nameof(foodCategoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null) 
                return NotFound();

            var usersDto = _mapper.Map<IEnumerable<UserIndexDto>>(users);

            var model = new UserIndexViewModel
            {
                Users = usersDto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> DoActiveOrPassive(int userId)
        {
            if (userId != 1)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                user.IsActive = user.IsActive ? false : true;
                
                var updatedUser = await _userManager.UpdateAsync(user);
                var result = JsonSerializer.Serialize(updatedUser);

                return Json(result);
            }
            
            return Json("Admin silinemez.");
        }
    }
}
