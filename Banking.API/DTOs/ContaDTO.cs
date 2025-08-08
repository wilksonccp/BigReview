namespace Banking.API.DTOs;

public class ContaDTO
{
    public record CriarContaDTO(
        Guid ClienteId,
        string NumeroAgencia,
        string NumeroConta,
        decimal SaldoInicial = 0m);
    public record ContaResponseDTO(
        Guid Id,
        Guid ClienteId,
        string Agencia,
        string Numero,
        decimal Saldo,
        string Status);

    public record OperacaoValorDTO(
        decimal Valor); // usado em depósito/saque
    public record TransferenciaDTO(
        Guid ContaOrigemId,
        Guid ContaDestinoId,
        decimal Valor);

    public record ExtratoFiltroDTO(
        DateTime? De,
        DateTime? Ate,
        int Page = 1,
        int PageSize = 50);
    public record ContasFiltroDTO(
        Guid? ClienteId,
        string? Status,
        int Page = 1,
        int PageSize = 50);

}
