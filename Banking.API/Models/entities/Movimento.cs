using Banking.API.Models.Entities;
using Banking.API.Models.Enums;

namespace Banking.API.Models.entities;

public class Movimento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ContaId { get; set; }
    public Conta Conta { get; set; } = null!;

    public DateTime Data { get; set; } = DateTime.UtcNow;
    public TipoMovimento Tipo { get; set; }
    public decimal Valor { get; set; }
    public string? Descricao { get; set; }
    public Guid? CorrelacaoId { get; set; }   // liga débito e crédito na transferência

    public static Movimento Debito(Guid contaId, decimal valor, string? desc = null, Guid? corr = null)
        => new() { ContaId = contaId, Tipo = TipoMovimento.Debito, Valor = valor, Descricao = desc, CorrelacaoId = corr };

    public static Movimento Credito(Guid contaId, decimal valor, string? desc = null, Guid? corr = null)
        => new() { ContaId = contaId, Tipo = TipoMovimento.Credito, Valor = valor, Descricao = desc, CorrelacaoId = corr };
}
