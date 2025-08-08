using Banking.API.Models.entities;
using Banking.API.Models.Entities;
using static Banking.API.DTOs.ContaDTO;
namespace Banking.API.Services;

public interface IContaService
{
    Task<Conta> CriarContaAsync(CriarContaDTO dto, CancellationToken ct);
    Task<Conta?> ObterPorIdAsync(Guid id, CancellationToken ct);
    Task<PagedResult<Conta>> ListarAsync(ContasFiltroDTO filtro, CancellationToken ct);

    Task<Conta> DepositarAsync(Guid contaId, decimal valor, CancellationToken ct);
    Task<Conta> SacarAsync(Guid contaId, decimal valor, CancellationToken ct);
    Task TransferirAsync(Guid contaOrigemId, Guid contaDestinoId, decimal valor, CancellationToken ct);

    Task<Conta> BloquearAsync(Guid contaId, CancellationToken ct);
    Task<Conta> DesbloquearAsync(Guid contaId, CancellationToken ct);
    Task EncerrarAsync(Guid contaId, CancellationToken ct);

    Task<PagedResult<Movimento>> ExtratoAsync(Guid contaId, DateTime? de, DateTime? ate, int page, int pageSize, CancellationToken ct);
}