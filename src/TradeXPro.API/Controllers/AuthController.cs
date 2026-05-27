using Microsoft.AspNetCore.Mvc;
using TradeXPro.API.DTOs;
using TradeXPro.API.Services;
using TradeXPro.Data.Repositories.Interfaces;

namespace TradeXPro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    private readonly IAuthLogRepository _authLogRepo;
    private readonly IAuthFileLogger _fileLogger;

    public AuthController(IUserRepository userRepo, IAuthLogRepository authLogRepo, IAuthFileLogger fileLogger)
    {
        _userRepo = userRepo;
        _authLogRepo = authLogRepo;
        _fileLogger = fileLogger;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var user = await _userRepo.GetByEmailAsync(request.Email);
        if (user == null || user.PasswordHash != request.Password) // In production: use proper hashing
        {
            // Log failed login to DB and text file
            await _authLogRepo.LogAuthEventAsync("LOGIN", request.Email, ip, "Failed");
            await _fileLogger.LogAsync("LOGIN", request.Email, ip, "Failed");

            return Ok(new LoginResponse { Success = false, Message = "Invalid email or password" });
        }

        // Log successful login to DB and text file
        await _authLogRepo.LogAuthEventAsync("LOGIN", request.Email, ip, "Success");
        await _fileLogger.LogAsync("LOGIN", request.Email, ip, "Success");

        return Ok(new LoginResponse
        {
            Success = true,
            Message = "Login successful",
            Token = $"mock-jwt-token-{user.Id}",
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccountType = user.AccountType
            }
        });
    }

    [HttpPost("logout")]
    public async Task<ActionResult<ApiResponse>> Logout([FromQuery] string email)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        // Log logout to DB and text file
        await _authLogRepo.LogAuthEventAsync("LOGOUT", email, ip, "Success");
        await _fileLogger.LogAsync("LOGOUT", email, ip, "Success");

        return Ok(ApiResponse.Ok("Logged out successfully"));
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequest request)
    {
        if (await _userRepo.EmailExistsAsync(request.Email))
            return Ok(ApiResponse.Fail("Email already registered"));

        var user = new Data.Entities.User
        {
            Email = request.Email,
            PasswordHash = request.Password, // In production: hash this
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await _userRepo.AddAsync(user);
        return Ok(ApiResponse.Ok("Registration successful"));
    }
}
