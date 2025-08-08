namespace Banking.API.DTOs;

public record MovimentoDTO(
        Guid Id,
        Guid ContaId,
        DateTime Data,
        string Tipo,          // "Débito" ou "Crédito"
        decimal Valor,
        string? Descricao,
        Guid? CorrelacaoId
    );
