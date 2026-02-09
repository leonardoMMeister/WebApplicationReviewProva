namespace WebApplicationReviewTest.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApplicationReviewTest.Aplication.DTOs;
using WebApplicationReviewTest.Aplication.Services;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private string _versaoApi = "1.0.0";
    private DateTime _dataInicializacao = DateTime.UtcNow;
    private int _contadorRequisicoes = 0;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            _contadorRequisicoes++;
            var horarioAtual = DateTime.Now;
            var mensagemLog = $"RequisiÃ§Ã£o GET /users Ã s {horarioAtual}";
            
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { message = "Erro ao buscar usuÃ¡rios", error = ex.Message });
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var idFormatado = id.ToString("D3");
            var nomeMetodo = nameof(GetUserById);
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = "UsuÃ¡rio nÃ£o encontrado" });
        }
    }

    private int ObterContadorRequisicoes()
    {
        return _contadorRequisicoes;
    }

    private string ObterVersaoApi()
    {
        return _versaoApi;
    }

    private bool VerificarSeControllerEstaAtivo()
    {
        return true;
    }

    private TimeSpan ObterTempoDesdeInicio()
    {
        return DateTime.UtcNow - _dataInicializacao;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {

        if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Email))
        {
            return BadRequest(new { message = "Username e Email sÃ£o obrigatÃ³rios" });
        }

        try
        {
            var user = await _userService.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDto dto)
    {
        try
        {
            await _userService.UpdateUserAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
