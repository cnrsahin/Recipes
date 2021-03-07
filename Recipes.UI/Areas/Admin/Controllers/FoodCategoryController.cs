using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.Service.Core.Abstract;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.Service.Results.Concrete;
using Recipes.UI.Areas.Admin.Models;
using Recipes.UI.Dtos;
using Recipes.UI.Helpers.ImageAddDel;
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
        private readonly ImageHelper _imageHelper;
        private User Who => _userManager.GetUserAsync(HttpContext.User).Result;

        public FoodCategoryController(IFoodCategoryRepository foodCategoryRepository, IMapper mapper, UserManager<User> userManager, ImageHelper imageHelper)
        {
            _foodCategoryRepository = foodCategoryRepository ?? throw new ArgumentNullException(nameof(foodCategoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _imageHelper = imageHelper;
        }

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
                FoodCategoriesResult = new DataResult<IEnumerable<FoodCategoryIndexDto>>(ResultStatus.Success, "Liste Cekildi!", dto)
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
                FoodCategoriesResult = new DataResult<IEnumerable<FoodCategoryIndexDto>>(ResultStatus.Success, "Liste Cekildi!", dto)
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> Delete(int foodCategoryId)
        {
            bool isCategory = await _foodCategoryRepository.AnyAsync(c => c.Id == foodCategoryId);

            if (isCategory)
            {
                var foodCategory = await _foodCategoryRepository.GetAsync(x => x.Id == foodCategoryId);
                foodCategory.IsDeleted = true;

                await _foodCategoryRepository.UpdateAsync(foodCategory);

                var model = JsonSerializer.Serialize(new Result(ResultStatus.Success, "Çöp kutusuna taşındı"));

                return Json(model);
            }
            else
                return Json("Kategori bulunamadı");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> UnDelete(int foodCategoryId)
        {
            var foodCategory = await _foodCategoryRepository.GetAsync(x => x.Id == foodCategoryId);
            foodCategory.IsDeleted = false;

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
                FoodCategoriesResult = new DataResult<IEnumerable<FoodCategoryIndexDto>>(ResultStatus.Success, "Liste Cekildi!", dto)
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
                FoodCategoriesResult = new DataResult<IEnumerable<FoodCategoryIndexDto>>(ResultStatus.Success, "Liste Cekildi!", dto)
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

                var picFileName = await _imageHelper.UploadPhoto(foodCategory.CategoryName ,foodCategoryAddDto.PictureFile, PictureType.FoodCategoryPic);
                foodCategory.CategoryPicture = picFileName;
                await _foodCategoryRepository.AddAsync(foodCategory);

                return Json("Onay bekleyenlere taşındı, yönetici onayından sonra yayınlanacaktır.");
            }

            return Json("Eksik bilgileri tamamlayınız!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<PartialViewResult> Update(int categoryId)
        {
            var category = await _foodCategoryRepository.GetAsync(c => c.Id == categoryId);
            var model = _mapper.Map<FoodCategoryUpdateDto>(category);
            return PartialView("_FoodCategoryUpdatePartialView", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(FoodCategoryUpdateDto foodCategoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var foodCategory = _mapper.Map<FoodCategory>(foodCategoryUpdateDto);
                foodCategory.UserId = Who.Id;
                foodCategory.IsConfirmed = true;
                await _foodCategoryRepository.UpdateAsync(foodCategory);

                return Json("Kategori Güncellendi");
            }

            return Json("Hata");
        }
    }
}
