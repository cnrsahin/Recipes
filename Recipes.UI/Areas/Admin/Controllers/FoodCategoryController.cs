using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class FoodCategoryController : Controller
    {
        private readonly IFoodCategoryRepository _foodCategoryRepository;

        public FoodCategoryController(IFoodCategoryRepository foodCategoryRepository)
        {
            _foodCategoryRepository = foodCategoryRepository ?? throw new ArgumentNullException(nameof(foodCategoryRepository));
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _foodCategoryRepository.GetAllAsync(c => !c.IsDeleted && c.IsConfirmed, c => c.User);

            if (categories == null) return NotFound();

            var model = new FoodCategoryIndexViewModel
            {
                FoodCategories = categories
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> GetRemoved()
        {
            var categories = await _foodCategoryRepository.GetAllAsync(c => c.IsDeleted && c.IsConfirmed, c => c.User);

            if (categories == null) return NotFound();

            var model = new FoodCategoryIndexViewModel
            {
                FoodCategories = categories
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> DeleteOrUndoDelete(int foodCategoryId, bool isWantDelete)
        {
            var foodCategory = await _foodCategoryRepository.GetAsync(x => x.Id == foodCategoryId);
            foodCategory.IsDeleted = isWantDelete;

            var trashedFoodCategory = await _foodCategoryRepository.UpdateAsync(foodCategory);

            var result = JsonSerializer.Serialize(trashedFoodCategory);

            return Json(result);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> WaitConfirm()
        {
            var categories = await _foodCategoryRepository.GetAllAsync(c => !c.IsDeleted && !c.IsConfirmed, c => c.User);

            if (categories == null) return NotFound();

            var model = new FoodCategoryIndexViewModel
            {
                FoodCategories = categories
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> NotConfirmed()
        {
            var categories = await _foodCategoryRepository.GetAllAsync(c => c.IsDeleted && !c.IsConfirmed, c => c.User);

            if (categories == null) return NotFound();

            var model = new FoodCategoryIndexViewModel
            {
                FoodCategories = categories
            };

            return View(model);
        }
    }
}
