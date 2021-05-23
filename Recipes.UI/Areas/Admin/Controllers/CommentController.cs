using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Service.Core.Abstract;
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
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var comments = await _commentRepository.GetAllAsync(c => !c.IsDeleted && c.IsConfirmed, c => c.Recipe, c => c.User);
            if (comments == null) return NotFound();

            var dto = _mapper.Map<IEnumerable<CommentIndexDto>>(comments);

            foreach (var item in dto)
            {
                var commentSelected = comments.FirstOrDefault(x => x.Id == item.Id);
                item.UserName = commentSelected.User.UserName;
                item.RecipeName = commentSelected.Recipe.FoodName;
            }

            var model = new CommentIndexViewModel
            {
                Comments = dto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> GetRemoved()
        {
            var comments = await _commentRepository.GetAllAsync(c => c.IsDeleted && c.IsConfirmed, c => c.Recipe, c => c.User);
            if (comments == null) return NotFound();

            var dto = _mapper.Map<IEnumerable<CommentIndexDto>>(comments);

            foreach (var item in dto)
            {
                var commentSelected = comments.FirstOrDefault(x => x.Id == item.Id);
                item.UserName = commentSelected.User.UserName;
                item.RecipeName = commentSelected.Recipe.FoodName;
            }

            var model = new CommentIndexViewModel
            {
                Comments = dto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> Delete(int commentId)
        {
            var comment = await _commentRepository.GetAsync(x => x.Id == commentId);
            comment.IsDeleted = true;

            await _commentRepository.UpdateAsync(comment);

            return Json("Test");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> UnDelete(int commentId)
        {
            var comment = await _commentRepository.GetAsync(x => x.Id == commentId);
            comment.IsDeleted = false;

            await _commentRepository.UpdateAsync(comment);

            return Json("Test");
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> WaitConfirm()
        {
            var comments = await _commentRepository.GetAllAsync(c => !c.IsDeleted && !c.IsConfirmed, c => c.Recipe, c => c.User);
            if (comments == null) return NotFound();

            var dto = _mapper.Map<IEnumerable<CommentIndexDto>>(comments);

            foreach (var item in dto)
            {
                var commentSelected = comments.FirstOrDefault(x => x.Id == item.Id);
                item.UserName = commentSelected.User.UserName;
                item.RecipeName = commentSelected.Recipe.FoodName;
            }

            var model = new CommentIndexViewModel
            {
                Comments = dto
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> NotConfirmed()
        {
            var comments = await _commentRepository.GetAllAsync(c => c.IsDeleted && !c.IsConfirmed, c => c.Recipe, c => c.User);
            if (comments == null) return NotFound();

            var dto = _mapper.Map<IEnumerable<CommentIndexDto>>(comments);

            foreach (var item in dto)
            {
                var commentSelected = comments.FirstOrDefault(x => x.Id == item.Id);
                item.UserName = commentSelected.User.UserName;
                item.RecipeName = commentSelected.Recipe.FoodName;
            }

            var model = new CommentIndexViewModel
            {
                Comments = dto
            };

            return View(model);
        }
    }
}
