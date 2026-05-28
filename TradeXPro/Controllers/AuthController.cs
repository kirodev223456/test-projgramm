using Microsoft.AspNetCore.Mvc;
using TradeXPro.Services;

namespace TradeXPro.Controllers;

public class AuthController : Controller
{
    private readonly ApiService _api;

    public AuthController(ApiService api)
    {
        _api = api;
    }

    public IActionResult Login()
    {
        // If already logged in, redirect to dashboard
        if (HttpContext.Session.GetString("UserEmail") != null)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        try
        {
            var response = await _api.PostAsync<LoginResponse>("api/auth/login", new
            {
                Email = email,
                Password = password
            });

            if (response != null && response.Success)
            {
                // Store user info in session
                HttpContext.Session.SetString("UserEmail", response.User?.Email ?? email);
                HttpContext.Session.SetString("UserName", $"{response.User?.FirstName} {response.User?.LastName}");
                HttpContext.Session.SetString("UserToken", response.Token ?? "");

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = response?.Message ?? "Invalid email or password";
            return View();
        }
        catch (Exception)
        {
            // API might be down - fall back to mockup redirect for development
            ViewBag.Error = "Unable to connect to server. Please try again.";
            return View();
        }
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string firstName, string lastName, string email, string password)
    {
        try
        {
            var response = await _api.PostAsync<ApiResponseDto>("api/auth/register", new
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            });

            if (response != null && response.Success)
            {
                TempData["SuccessMessage"] = "Account created! Please sign in.";
                return RedirectToAction("Login");
            }

            ViewBag.Error = response?.Message ?? "Registration failed";
            return View();
        }
        catch (Exception)
        {
            ViewBag.Error = "Unable to connect to server. Please try again.";
            return View();
        }
    }

    public async Task<IActionResult> Logout()
    {
        var email = HttpContext.Session.GetString("UserEmail") ?? "unknown";

        try
        {
            // Call API to log the logout event (writes to text file + DB)
            await _api.PostAsync<ApiResponseDto>($"api/auth/logout?email={email}", new { });
        }
        catch
        {
            // If API is down, still proceed with local logout
        }

        // Clear session
        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }
}

// DTOs for deserializing API responses
public class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Token { get; set; }
    public UserDto? User { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty;
}

public class ApiResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
