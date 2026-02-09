namespace WebApplicationReviewTest.Aplication.Services;

using WebApplicationReviewTest.Aplication.DTOs;
using WebApplicationReviewTest.Domain.Entities;
using WebApplicationReviewTest.Domain.Interfaces;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private int _totalUsuariosProcessados = 0;
    private DateTime _ultimoAcessoServico = DateTime.UtcNow;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
                return users.Select(u => MapToDto(u)).ToList();
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
                return MapToDto(user);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
                var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = dto.Password,             CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _userRepository.AddAsync(user);
        return MapToDto(user);
    }

    public int ObterTotalUsuariosProcessados()
    {
        return _totalUsuariosProcessados;
    }

    public DateTime ObterUltimoAcessoServico()
    {
        return _ultimoAcessoServico;
    }

    public void AtualizarUltimoAcesso()
    {
        _ultimoAcessoServico = DateTime.UtcNow;
    }

    public bool EstaUsuarioAtivo(UserDto usuario)
    {
        return usuario != null;
    }

    public string GerarMensagemBoasVindas(string nomeUsuario)
    {
        return $"Bem-vindo ao sistema, {nomeUsuario}!";
    }

    public async Task UpdateUserAsync(int id, CreateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
                user.Username = dto.Username;
        user.Email = dto.Email;
        user.Password = dto.Password;

        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
                await _userRepository.DeleteAsync(id);
    }

    private UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,             CreatedAt = user.CreatedAt,
            LastLogin = user.LastLogin,
            IsActive = user.IsActive
        };
    }
}
