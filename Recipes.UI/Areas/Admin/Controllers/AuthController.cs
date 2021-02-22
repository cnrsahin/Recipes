using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.UI.Dtos;
using System;
using System.Threading.Tasks;

namespace Recipes.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        public IActionResult Login()
        {
            var isSignedIn = _signInManager.IsSignedIn(HttpContext.User);

            if (isSignedIn) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı!");
                        return View();
                    }

                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı Adı veya Şifre hatalı");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı!");
                    return View();
                }
            }
            else
                return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [Authorize]
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }
    }
}
