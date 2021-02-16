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
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var comments = await _commentRepository.GetAllAsync(c => !c.IsDeleted && c.IsConfirmed, c => c.Recipe, c=>  c.User);

            if (comments == null) return NotFound();

            var model = new CommentIndexViewModel
            {
                Comments = comments
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> GetRemoved()
        {
            var comments = await _commentRepository.GetAllAsync(c => c.IsDeleted && c.IsConfirmed, c => c.Recipe, c=> c.User);

            if (comments == null) return NotFound();

            var model = new CommentIndexViewModel
            {
                Comments = comments
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> DeleteOrUndoDelete(int commentId, bool isWantDelete)
        {
            var comment = await _commentRepository.GetAsync(x => x.Id == commentId);
            comment.IsDeleted = isWantDelete;

            var trashedComment = await _commentRepository.UpdateAsync(comment);

            var result = JsonSerializer.Serialize(trashedComment);

            return Json(result);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> WaitConfirm()
        {
            var comments = await _commentRepository.GetAllAsync(c => !c.IsDeleted && !c.IsConfirmed, c => c.Recipe, c => c.User);

            if (comments == null) return NotFound();

            var model = new CommentIndexViewModel
            {
                Comments = comments
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> NotConfirmed()
        {
            var comments = await _commentRepository.GetAllAsync(c => c.IsDeleted && !c.IsConfirmed, c => c.Recipe, c => c.User);

            if (comments == null) return NotFound();

            var model = new CommentIndexViewModel
            {
                Comments = comments
            };

            return View(model);
        }
    }
}
