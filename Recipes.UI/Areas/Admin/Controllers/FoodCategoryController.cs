using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class FoodCategoryController : Controller
    {
        private readonly IFoodCategoryRepository _foodCategoryRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public FoodCategoryController(IFoodCategoryRepository foodCategoryRepository, IMapper mapper, UserManager<User> userManager)
        {
            _foodCategoryRepository = foodCategoryRepository ?? throw new ArgumentNullException(nameof(foodCategoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        private User Who => _userManager.GetUserAsync(HttpContext.User).Result;

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _foodCategoryRepository.GetAllAsync(c => !c.IsDeleted && c.IsConfirmed, c => c.User);
            if (categories == null)
                return NotFound();

            var dto = _mapper.Map<IEnumerable<FoodCategoryIndexDto>>(categories);

            foreach (var item in dto)
            {
                var categorySelected = categories.FirstOrDefault(x => x.Id == item.Id);
                item.UserName = categorySelected.User.UserName;
            }

            var model = new FoodCategoryIndexViewModel
            {
                FoodCategories = dto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> GetRemoved()
        {
            var categories = await _foodCategoryRepository.GetAllAsync(c => c.IsDeleted && c.IsConfirmed, c => c.User);
            if (categories == null)
                return NotFound();

            var dto = _mapper.Map<IEnumerable<FoodCategoryIndexDto>>(categories);

            foreach (var item in dto)
            {
                var categorySelected = categories.FirstOrDefault(x => x.Id == item.Id);
                item.UserName = categorySelected.User.UserName;
            }

            var model = new FoodCategoryIndexViewModel
            {
                FoodCategories = dto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> DeleteOrUndoDelete(int foodCategoryId, bool isWantDelete)
        {
            var foodCategory = await _foodCategoryRepository.GetAsync(x => x.Id == foodCategoryId);
            foodCategory.IsDeleted = isWantDelete;

            await _foodCategoryRepository.UpdateAsync(foodCategory);

            return Json("Test");
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> WaitConfirm()
        {
            var categories = await _foodCategoryRepository.GetAllAsync(c => !c.IsDeleted && !c.IsConfirmed, c => c.User);
            if (categories == null)
                return NotFound();

            var dto = _mapper.Map<IEnumerable<FoodCategoryIndexDto>>(categories);

            foreach (var item in dto)
            {
                var categorySelected = categories.FirstOrDefault(x => x.Id == item.Id);
                item.UserName = categorySelected.User.UserName;
            }

            var model = new FoodCategoryIndexViewModel
            {
                FoodCategories = dto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> NotConfirmed()
        {
            var categories = await _foodCategoryRepository.GetAllAsync(c => c.IsDeleted && !c.IsConfirmed, c => c.User);
            if (categories == null)
                return NotFound();

            var dto = _mapper.Map<IEnumerable<FoodCategoryIndexDto>>(categories);

            foreach (var item in dto)
            {
                var categorySelected = categories.FirstOrDefault(x => x.Id == item.Id);
                item.UserName = categorySelected.User.UserName;
            }

            var model = new FoodCategoryIndexViewModel
            {
                FoodCategories = dto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public PartialViewResult Add()
        {
            return PartialView("_FoodCategoryAddPartialView");
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPost]
        public async Task<IActionResult> Add(FoodCategoryAddDto foodCategoryAddDto)
        {
            if (ModelState.IsValid)
            {

                var foodCategory = _mapper.Map<FoodCategory>(foodCategoryAddDto);
                foodCategory.UserId = Who.Id;
                await _foodCategoryRepository.AddAsync(foodCategory);

                return Json("Onay bekleyenlere taşındı, yönetici onayından sonra yayınlanacaktır.");
            }

            return Json("Hata!");
        }
    }
}
