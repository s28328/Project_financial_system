using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_financial_system.Models.Request;
using Project_financial_system.Services.Abstraction;

namespace Project_financial_system.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppUserController:ControllerBase
{
    private readonly IAppUserService _userService;

    public AppUserController(IAppUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudent(RegisterRequest model, CancellationToken cancellationToken)
    {
        await _userService.RegisterUser(model, cancellationToken);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var result = await _userService.LoginUser(loginRequest, cancellationToken);

        return Ok(result);
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken, CancellationToken cancellationToken)
    {
        var result = await _userService.RefreshUserToken(refreshToken, cancellationToken);
        return Ok(result);
    }
}