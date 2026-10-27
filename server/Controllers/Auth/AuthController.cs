using Microsoft.AspNetCore.Mvc;
using CommBank.Services;

namespace CommBank.Controllers;

public record LoginInput(string Email, string Password);

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) =>
        _authService = authService;

    // ==========================================
    // AUTHENTICATION (Login) -> POST: api/Auth/Login
    // Email aur password verify karke user authentication karta hai
    // ==========================================
    [HttpPost("Login")]
    public async Task<IActionResult> Post(LoginInput input)
    {
        var user = await _authService.Login(input.Email, input.Password);

        if (user is null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
