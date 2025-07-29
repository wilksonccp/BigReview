using Banking.API.Models;


namespace Banking.API.Repositories;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ListarTodosAsync();
    Task<Cliente?> ObterPorIdAsync(Guid id);
    Task<Cliente> AdicionarAsync(Cliente cliente);
    Task<Cliente?> UpdateAsync(Guid id, Cliente cliente);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<IEnumerable<Cliente>> BuscarPorNomeAsync(string nome);

}
