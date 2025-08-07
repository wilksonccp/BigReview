using Microsoft.AspNetCore.Mvc;
using SecureNotes.API.DTOs;
using SecureNotes.API.Services;
using SecureNotes.API.Models;
namespace SecureNotes.API.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        var usuario = await _authService.RegisterAsync(dto);
        return CreatedAtAction(nameof(Register), new { id = usuario.Id, usuario.Nome, usuario.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        try
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }
}


