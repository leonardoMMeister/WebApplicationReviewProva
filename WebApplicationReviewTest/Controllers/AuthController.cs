namespace WebApplicationReviewTest.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApplicationReviewTest.Aplication.DTOs;
using WebApplicationReviewTest.Aplication.Services;
using WebApplicationReviewTest.Domain.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly UserService _userService;

    public AuthController(IAuthenticationService authenticationService, UserService userService)
    {
        _authenticationService = authenticationService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
        {
            return BadRequest("Username e Password são obrigatórios");
        }

        var isAuthenticated = await _authenticationService.AuthenticateAsync(loginDto.Username, loginDto.Password);
        
        if (!isAuthenticated)
        {
            return Unauthorized(new { message = "Credenciais inválidas" });
        }

        var user = await _authenticationService.GetAuthenticatedUserAsync(loginDto.Username);
        user.LastLogin = DateTime.UtcNow;

        var response = new LoginResponseDto
        {
            Success = true,
            Message = "Login realizado com sucesso",
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            }
        };

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            var user = await _userService.CreateUserAsync(createUserDto);
            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
