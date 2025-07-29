using Banking.API.Models;
namespace Banking.API.Services;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> ListarTodosAsync();
    Task<Cliente?> ObterPorIdAsync(Guid id);
    Task<Cliente> AdicionarAsync(Cliente cliente);
    Task<Cliente?> AtualizarAsync(Guid id, Cliente cliente);
    Task<bool> RemoverAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<IEnumerable<Cliente>> BuscarPorNomeAsync(string nome);
}
