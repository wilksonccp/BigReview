using Banking.API.Models.entities;
using Banking.API.Repositories;
using Banking.API.Validations;
using FluentValidation;
namespace Banking.API.Services;

public class ClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IValidator<Cliente> _validator;
    public ClienteService(
        IClienteRepository clienteRepository,
        IValidator<Cliente> validator)
    {
        _clienteRepository = clienteRepository;
        _validator = validator;
    }
    public async Task<IEnumerable<Cliente>> ListarTodosAsync()
    {
        return await _clienteRepository.ListarTodosAsync();
    }
    public async Task<Cliente?> ObterPorIdAsync(Guid id)
    {
        return await _clienteRepository.ObterPorIdAsync(id);
    }
    public async Task<Cliente> AdicionarAsync(Cliente cliente)
    {
        var validationResult = await _validator.ValidateAsync(cliente);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _clienteRepository.AdicionarAsync(cliente);
    }
    public async Task<Cliente?> AtualizarAsync(Guid id, Cliente cliente)
    {
        if(!await _clienteRepository.ExistsAsync(id))
        {
            return null; // Cliente não encontrado
        }
        var validationResult = await _validator.ValidateAsync(cliente);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _clienteRepository.UpdateAsync(id, cliente);
    }
    public async Task<bool> RemoverAsync(Guid id)
    {
        return await _clienteRepository.DeleteAsync(id);
    }
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _clienteRepository.ExistsAsync(id);
    }
    public async Task<IEnumerable<Cliente>> BuscarPorNomeAsync(string nome)
    {
        return await _clienteRepository.BuscarPorNomeAsync(nome);
    }
}
