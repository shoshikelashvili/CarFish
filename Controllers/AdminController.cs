using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CarFish.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CarFish.Controllers
{
    public class AdminController : Controller
    {
        //private readonly UserManager<IdentityUser> userManager;
        //private readonly SignInManager<IdentityUser> signInManager;

        public AdminController()
        {

        }

        [Route("admin")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCheck(UserLoginDto model)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = await userManager.FindByNameAsync(model.Name);
            //    if (await userManager.CheckPasswordAsync(user, model.Password) == false)
            //    {
            //        ModelState.AddModelError("message", "Invalid credentials");
            //        return View("Login");
            //    }

                //var result = await signInManager.PasswordSignInAsync(model.Name, model.Password, true, false);

            if (model.Name == "greenback" && model.Password=="greenb@ckDOTA123")
            {
                Response.Cookies.Append("is_admin", "true");
                return Redirect("/dashboard");
            }
            else
            {
                return Redirect("/404");
            }
            //}
            return View("Login");
        }

        public async Task<IActionResult> Logout()
        {
            //await signInManager.SignOutAsync();
            Response.Cookies.Delete("is_admin");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
