using AgriEnergyConnect.Models.ViewModels;
using AgriEnergyConnect.Services;
using Microsoft.AspNetCore.Mvc;

/**Code Attribute
    /* https://stackoverflow.com/questions/1015813/what-goes-into-the-controller-in-mvc
    /*Author: victor hugo*/


namespace AgriEnergyConnect.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(AuthService authService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await _authService.AuthenticateAsync(model.Username, model.Password);
                
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(model);
                }

                // Set session variables
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("RoleId", user.RoleId);

                // Redirect based on role
                return user.RoleId switch
                {
                    1 => RedirectToAction("Index", "Farmer"),    // Farmer
                    2 => RedirectToAction("Index", "Employee"),  // Employee
                    _ => RedirectToAction("Index", "Home")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed");
                ModelState.AddModelError("", "An error occurred during login");
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
