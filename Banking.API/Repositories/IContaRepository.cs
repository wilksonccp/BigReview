using Banking.API.Models;
namespace Banking.API.Repositories;

public interface IContaRepository
{
    Task<IEnumerable<Conta>> ListarTodosAsync();
    Task<Conta?> ObterPorIdAsync(int id);
    Task<Conta> AdicionarAsync(Conta conta);
    Task<Conta?> UpdateAsync(int id, Conta conta);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Conta>> ListarPorClienteIdAsync(Guid clienteId);
}
