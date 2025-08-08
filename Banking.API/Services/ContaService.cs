using Banking.API.Models.entities;
using Banking.API.Models.Entities;
using Banking.API.Models.Enums;
using Banking.API.Repositories;
using Banking.API.Validations;
using FluentValidation;
using static Banking.API.DTOs.ContaDTO;
namespace Banking.API.Services;

public class ContaService : IContaService
{
    private readonly BankingContext _ctx;

    public ContaService(BankingContext ctx) => _ctx = ctx;

    public async Task<Conta> CriarContaAsync(CriarContaDTO dto, CancellationToken ct)
    {
        // valida cliente
        bool clienteExiste = await _ctx.Clientes.AnyAsync(c => c.Id == dto.ClienteId, ct);
        if (!clienteExiste) throw new ArgumentException("Cliente não encontrado.");

        // agencia+numero único
        bool ocupado = await _ctx.Contas.AnyAsync(c => c.Agencia == dto.NumeroAgencia && c.Numero == dto.NumeroConta, ct);
        if (ocupado) throw new InvalidOperationException("Já existe conta com esta agência/número.");

        var conta = new Conta
        {
            ClienteId = dto.ClienteId,
            Agencia = dto.NumeroAgencia,
            Numero = dto.NumeroConta,
            Moeda = dto.Moeda
        };

        if (dto.SaldoInicial > 0)
            conta.Depositar(dto.SaldoInicial);

        _ctx.Contas.Add(conta);
        await _ctx.SaveChangesAsync(ct);

        return conta;
    }

    public Task<Conta?> ObterPorIdAsync(Guid id, CancellationToken ct)
        => _ctx.Contas.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<PagedResult<Conta>> ListarAsync(ContasFiltroDTO f, CancellationToken ct)
    {
        var q = _ctx.Contas.AsNoTracking().AsQueryable();
        if (f.ClienteId is { } cid) q = q.Where(x => x.ClienteId == cid);
        if (!string.IsNullOrWhiteSpace(f.Status) && Enum.TryParse<StatusConta>(f.Status, true, out var st))
            q = q.Where(x => x.Status == st);

        int total = await q.CountAsync(ct);
        var items = await q.OrderBy(x => x.Agencia).ThenBy(x => x.Numero)
                           .Skip((f.Page - 1) * f.PageSize)
                           .Take(f.PageSize)
                           .ToListAsync(ct);
        return new(items, total, f.Page, f.PageSize);
    }

    public async Task<Conta> DepositarAsync(Guid contaId, decimal valor, CancellationToken ct)
    {
        var conta = await _ctx.Contas.Include(c => c.Movimentos).FirstOrDefaultAsync(c => c.Id == contaId, ct)
                    ?? throw new KeyNotFoundException("Conta não encontrada.");
        conta.Depositar(valor);
        await _ctx.SaveChangesAsync(ct);
        return conta;
    }

    public async Task<Conta> SacarAsync(Guid contaId, decimal valor, CancellationToken ct)
    {
        var conta = await _ctx.Contas.Include(c => c.Movimentos).FirstOrDefaultAsync(c => c.Id == contaId, ct)
                    ?? throw new KeyNotFoundException("Conta não encontrada.");
        conta.Sacar(valor);
        await _ctx.SaveChangesAsync(ct);
        return conta;
    }

    public async Task TransferirAsync(Guid origemId, Guid destinoId, decimal valor, CancellationToken ct)
    {
        if (origemId == destinoId) throw new ArgumentException("Contas devem ser diferentes.");
        if (valor <= 0) throw new ArgumentOutOfRangeException(nameof(valor));

        await using var tx = await _ctx.Database.BeginTransactionAsync(ct);

        var origem = await _ctx.Contas.Include(c => c.Movimentos).FirstOrDefaultAsync(c => c.Id == origemId, ct)
                      ?? throw new KeyNotFoundException("Conta de origem não encontrada.");
        var destino = await _ctx.Contas.Include(c => c.Movimentos).FirstOrDefaultAsync(c => c.Id == destinoId, ct)
                      ?? throw new KeyNotFoundException("Conta de destino não encontrada.");

        if (!string.Equals(origem.Moeda, destino.Moeda, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Moedas diferentes.");

        // mesmo CorrelationId nos dois movimentos
        var corr = Guid.NewGuid();
        origem.Sacar(valor);
        origem.Movimentos[^1].CorrelacaoId = corr;

        destino.Depositar(valor);
        destino.Movimentos[^1].CorrelacaoId = corr;

        await _ctx.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);
    }

    public async Task<Conta> BloquearAsync(Guid contaId, CancellationToken ct)
    {
        var conta = await _ctx.Contas.FirstOrDefaultAsync(c => c.Id == contaId, ct)
                    ?? throw new KeyNotFoundException("Conta não encontrada.");
        conta.Bloquear();
        await _ctx.SaveChangesAsync(ct);
        return conta;
    }

    public async Task<Conta> DesbloquearAsync(Guid contaId, CancellationToken ct)
    {
        var conta = await _ctx.Contas.FirstOrDefaultAsync(c => c.Id == contaId, ct)
                    ?? throw new KeyNotFoundException("Conta não encontrada.");
        conta.Desbloquear();
        await _ctx.SaveChangesAsync(ct);
        return conta;
    }

    public async Task EncerrarAsync(Guid contaId, CancellationToken ct)
    {
        var conta = await _ctx.Contas.FirstOrDefaultAsync(c => c.Id == contaId, ct)
                    ?? throw new KeyNotFoundException("Conta não encontrada.");
        conta.Encerrar();
        await _ctx.SaveChangesAsync(ct);
    }

    public async Task<PagedResult<Movimento>> ExtratoAsync(Guid contaId, DateTime? de, DateTime? ate, int page, int pageSize, CancellationToken ct)
    {
        var q = _ctx.Movimentos.AsNoTracking().Where(m => m.ContaId == contaId);
        if (de.HasValue) q = q.Where(m => m.Data >= de.Value);
        if (ate.HasValue) q = q.Where(m => m.Data <= ate.Value);

        var total = await q.CountAsync(ct);
        var items = await q.OrderByDescending(m => m.Data)
                           .Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .ToListAsync(ct);
        return new(items, total, page, pageSize);
    }
}

