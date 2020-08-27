using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtProjectClient.ApiServices.Concrete;
using JwtProjectClient.ApiServices.Interfaces;
using JwtProjectClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtProjectClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;

        }
        public async Task<IActionResult> SignIn()
        {
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLogin appUserLogin)
        {
            if (ModelState.IsValid)
            {
                if(await _authService.LogIn(appUserLogin))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Username or password is incorrect.");
            }
            return View(appUserLogin);

        }
    }
}
