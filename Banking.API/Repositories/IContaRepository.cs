using Banking.API.Models.Entities;

public interface IContaRepository
{
    Task<Conta?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Conta conta, CancellationToken ct = default);
    Task UpdateAsync(Conta conta, CancellationToken ct = default);
    Task<bool> ExistsNumeroAsync(string agencia, string numero, CancellationToken ct = default);

    Task<PagedResult<Conta>> SearchAsync(ContasFiltro filtro, CancellationToken ct = default);

    // Transferência/concorrência será tratada no Service com transação (DbContext)
}

// Models/support
public sealed record ContasFiltro(Guid? ClienteId, string? Status, int Page, int PageSize);
public sealed record PagedResult<T>(IReadOnlyList<T> Items, int Total, int Page, int PageSize);
