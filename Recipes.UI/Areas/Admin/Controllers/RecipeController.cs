using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeRepository.GetAllAsync(x => !x.IsDeleted && x.IsConfirmed, x => x.User, x => x.FoodCategory);
            if (recipes == null) 
                return NotFound();

            var recipeDto = _mapper.Map<IEnumerable<RecipeIndexDto>>(recipes);

            foreach (var recipe in recipeDto)
            {
                var recipeSelected = recipes.FirstOrDefault(r => r.Id == recipe.Id);
                recipe.FoodCategoryName = recipeSelected.FoodCategory.CategoryName;
                recipe.UserName = recipeSelected.User.UserName;
            }

            var model = new RecipeIndexViewModel
            {
                Recipes = recipeDto
            };

            return View(model);

        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> GetRemoved()
        {
            var recipes = await _recipeRepository.GetAllAsync(x => x.IsDeleted && x.IsConfirmed, x => x.User, x => x.FoodCategory);
            if (recipes == null) 
                return NotFound();

            var recipeDto = _mapper.Map<IEnumerable<RecipeIndexDto>>(recipes);

            foreach (var recipe in recipeDto)
            {
                var recipeSelected = recipes.FirstOrDefault(r => r.Id == recipe.Id);
                recipe.FoodCategoryName = recipeSelected.FoodCategory.CategoryName;
                recipe.UserName = recipeSelected.User.UserName;
            }

            var model = new RecipeIndexViewModel
            {
                Recipes = recipeDto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> WaitConfirm()
        {
            var recipes = await _recipeRepository.GetAllAsync(x => !x.IsDeleted && !x.IsConfirmed, x => x.User, x => x.FoodCategory);
            if (recipes == null) 
                return NotFound();

            var recipeDto = _mapper.Map<IEnumerable<RecipeIndexDto>>(recipes);

            foreach (var recipe in recipeDto)
            {
                var recipeSelected = recipes.FirstOrDefault(r => r.Id == recipe.Id);
                recipe.FoodCategoryName = recipeSelected.FoodCategory.CategoryName;
                recipe.UserName = recipeSelected.User.UserName;
            }

            var model = new RecipeIndexViewModel
            {
                Recipes = recipeDto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> NotConfirmed()
        {
            var recipes = await _recipeRepository.GetAllAsync(x => x.IsDeleted && !x.IsConfirmed, x => x.User, x => x.FoodCategory);
            if (recipes == null) 
                return NotFound();

            var recipeDto = _mapper.Map<IEnumerable<RecipeIndexDto>>(recipes);

            foreach (var recipe in recipeDto)
            {
                var recipeSelected = recipes.FirstOrDefault(r => r.Id == recipe.Id);
                recipe.FoodCategoryName = recipeSelected.FoodCategory.CategoryName;
                recipe.UserName = recipeSelected.User.UserName;
            }

            var model = new RecipeIndexViewModel
            {
                Recipes = recipeDto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> DeleteOrUndoDelete(int recipeId, bool isWantDelete)
        {
            var recipe = await _recipeRepository.GetAsync(x => x.Id == recipeId);
            recipe.IsDeleted = isWantDelete;

            var trashedRecipe = await _recipeRepository.UpdateAsync(recipe);
            var result = JsonSerializer.Serialize(trashedRecipe);

            return Json(result);
        }
    }
}
