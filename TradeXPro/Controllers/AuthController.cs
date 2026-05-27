using Microsoft.AspNetCore.Mvc;

namespace TradeXPro.Controllers;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        // Mockup: just redirect to dashboard
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string fullName, string email, string password)
    {
        // Mockup: redirect to login
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        // Mockup: redirect to login
        return RedirectToAction("Login");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }
}
