using SecureNotes.API.Models;
namespace SecureNotes.API.Repositories;

public class FakeUserRepository
{
    private readonly List<Usuario> _usuarios = new();
    private int _idCounter = 1;

    public Task<Usuario> GetByEmailAsync(string email)
    {
        return Task.FromResult(_usuarios.FirstOrDefault(u => u.Email == email));
    }

    public Task<Usuario> CrierUsuarioAsync(Usuario usuario)
    {
        usuario.Id = _idCounter++;
        _usuarios.Add(usuario);
        return Task.FromResult(usuario);
    }
}
// This class simulates a user repository using an in-memory list.