using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MvcCookieAuthSample.ViewModels;
using Microsoft.AspNetCore.Identity;
using MvcCookieAuthSample.Models;

namespace MvcCookieAuthSample.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;//用户创建的管理对象
        private SignInManager<ApplicationUser> _signInManager;//登录管理的对象

        private IActionResult RedirectToRurl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        private void ShowErrorSummary(IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _userManager.FindByNameAsync(loginModel.Email);

                var passwordReuslt = _userManager.PasswordHasher.VerifyHashedPassword(applicationUser, applicationUser.PasswordHash, loginModel.Password);//用来验证密码正确性,返回值是一个enum

                if (applicationUser == null)
                {
                    ModelState.AddModelError(string.Empty, "找不到用户");
                    return View();
                }
                var signInResult = await _signInManager.PasswordSignInAsync(applicationUser, loginModel.Password, false, false);//也可以用来验证密码
                if (signInResult.Succeeded)
                {
                    return RedirectToRurl(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "不知道啥原因");
                }
            }
            return View();
        }

        public IActionResult Register(string returnUrl1)
        {
            ViewData["ReturnUrl"] = returnUrl1;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email,
                    NormalizedUserName = registerViewModel.Email
                };

                IdentityResult identityResult = await _userManager.CreateAsync(applicationUser, registerViewModel.Password);
                if (identityResult.Succeeded)
                {
                    await _signInManager.SignInAsync(applicationUser, new AuthenticationProperties() { IsPersistent = true });
                    return RedirectToRurl(returnUrl);
                }
                else
                {
                    ShowErrorSummary(identityResult);
                }
            }
            return View();
        }



        public IActionResult MakeLogin()
        {
            //3.指定验证成功的方式

            //3.1.需要先指定角色
            List<Claim> list = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"jesse"),
                new Claim(ClaimTypes.Role,"admin")
            };

            //3.2创建一个Identity
            //必须指定Authentication类型,否则不会识别到
            var claminIdentity = new ClaimsIdentity(list, CookieAuthenticationDefaults.AuthenticationScheme);

            //3.3注册
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claminIdentity));

            return Ok();
        }

        public IActionResult LoginOut()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }


    }


}