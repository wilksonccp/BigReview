using SecureNotes.API.DTOs;
using SecureNotes.API.Models;
using SecureNotes.API.Repositories;
using BCrypt.Net;
namespace SecureNotes.API.Services;

public class AuthService
{
    private readonly FakeUserRepository _userRepository;
    private readonly TokenService _tokenService;

    public AuthService(FakeUserRepository userRepository, TokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> LoginAsync(LoginDTO dto)
    {
        var usuario = await _userRepository.GetByEmailAsync(dto.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
        {
            throw new UnauthorizedAccessException("Email ou senha inválidos.");
        }
        return _tokenService.GerarToken(usuario);
    }

    public async Task<Usuario> RegisterAsync(RegisterDTO dto)
    {
        if (await _userRepository.GetByEmailAsync(dto.Email) != null)
        {
            throw new InvalidOperationException("Email já cadastrado.");
        }
        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Role = string.IsNullOrWhiteSpace(dto.Role) ? "User" : dto.Role
        };
        return await _userRepository.CrierUsuarioAsync(usuario);
    }
}
