using Banking.API.Models;
using Banking.API.Repositories;
using Banking.API.Validations;
using FluentValidation;
namespace Banking.API.Services;

public class ContaService
{
    private readonly IContaRepository _contaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IValidator<Conta> _validator;
    public ContaService(
        IContaRepository contaRepository,
        IClienteRepository clienteRepository,
        IValidator<Conta> validator)
    {
        _contaRepository = contaRepository;
        _clienteRepository = clienteRepository;
        _validator = validator;
    }
    public async Task<IEnumerable<Conta>> ListarTodosAsync()
    {
        return await _contaRepository.ListarTodosAsync();
    }
    public async Task<Conta?> ObterPorIdAsync(int id)
    {
        return await _contaRepository.ObterPorIdAsync(id);
    }
    public async Task<Conta> AdicionarAsync(Conta conta)
    {
        var validationResult = await _validator.ValidateAsync(conta);
        var clienteExists = await _clienteRepository.ExistsAsync(conta.ClienteId);
        if (!clienteExists)
        {
            throw new ValidationException("Cliente não encontrado.");
        }
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _contaRepository.AdicionarAsync(conta);
    }
    public async Task<Conta?> AtualizarAsync(int id, Conta conta)
    {
        if (!await _contaRepository.ExistsAsync(id))
        {
            return null; // Conta não encontrada
        }
        var validationResult = await _validator.ValidateAsync(conta);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _contaRepository.UpdateAsync(id, conta);
    }
    public async Task<bool> RemoverAsync(int id)
    {
        return await _contaRepository.DeleteAsync(id);
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _contaRepository.ExistsAsync(id);
    }
    public async Task<IEnumerable<Conta>> ListarPorClienteIdAsync(Guid clienteId)
    {
        return await _contaRepository.ListarPorClienteIdAsync(clienteId);
    }
}
