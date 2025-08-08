namespace Banking.API.DTOs;

public record ContaResponseDTO(
       Guid Id,
       Guid ClienteId,
       string Agencia,
       string Numero,
       string Moeda,
       decimal Saldo,
       string Status,
       DateTime DataCriacao
   );
