using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebDinner.Models.ViewModels;
using WebDinner.Models.IdentityModels;

namespace WebDinner.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr, RoleManager<IdentityRole> roleMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMgr;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                    loginModel.Password, false, false)).Succeeded)
                    {
                        var r = await userManager.GetRolesAsync(user);
                        var n = r.FirstOrDefault();
                        if (n == "Admin")
                            return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
                        else
                            return Redirect(loginModel?.ReturnUrl ?? "/Home/GetFirstDishes");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        [AllowAnonymous]
        public ViewResult Register(string returnUrl)
        {
            return View(new RegisterModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser() { UserName = registerModel.Name };
                var result = await userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false); 
                    return Redirect(registerModel?.ReturnUrl ?? "/Home/GetFirstDishes");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}