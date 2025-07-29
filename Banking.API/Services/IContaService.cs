using Banking.API.Models;
namespace Banking.API.Services;

public interface IContaService
{
    Task<IEnumerable<Conta>> ListarTodosAsync();
    Task<Conta?> ObterPorIdAsync(int id);
    Task<Conta> AdicionarAsync(Conta conta);
    Task<Conta?> AtualizarAsync(int id, Conta conta);
    Task<bool> RemoverAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Conta>> ListarPorClienteIdAsync(Guid clienteId);
}
