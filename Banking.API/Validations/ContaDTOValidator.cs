using Banking.API.DTOs;
using FluentValidation;
using static Banking.API.DTOs.ContaDTO;

namespace Banking.API.Validations;
// Validator for CriarContaDTO
public class CriarContaDTOValidator : AbstractValidator<CriarContaDTO>
{
    public CriarContaDTOValidator()
    {
        RuleFor(x => x.ClienteId).NotEmpty();
        RuleFor(x => x.NumeroAgencia).NotEmpty().MaximumLength(10);
        RuleFor(x => x.NumeroConta).NotEmpty().MaximumLength(20);
        RuleFor(x => x.SaldoInicial).GreaterThanOrEqualTo(0);
    }
}

public class OperacaoValorDTOValidator : AbstractValidator<OperacaoValorDTO>
{
    public OperacaoValorDTOValidator()
    {
        RuleFor(x => x.Valor).GreaterThan(0);
    }
}

public class TransferenciaDTOValidator : AbstractValidator<TransferenciaDTO>
{
    public TransferenciaDTOValidator()
    {
        RuleFor(x => x.ContaOrigemId).NotEmpty();
        RuleFor(x => x.ContaDestinoId).NotEmpty().NotEqual(x => x.ContaOrigemId);
        RuleFor(x => x.Valor).GreaterThan(0);
    }
}

