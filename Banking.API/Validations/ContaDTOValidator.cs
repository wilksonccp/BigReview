using Banking.API.DTOs;
using FluentValidation;

namespace Banking.API.Validations;

public class ContaDTOValidator : AbstractValidator<ContaDTO>
{
    public ContaDTOValidator()
    {
        RuleFor(c => c.Numero)
            .NotEmpty().WithMessage("O número da conta é obrigatório")
            .Length(6, 10).WithMessage("O número da conta deve ter entre 6 e 10 dígitos");

        RuleFor(c => c.Saldo)
            .GreaterThanOrEqualTo(0).WithMessage("O saldo inicial não pode ser negativo");

        RuleFor(c => c.ClienteId)
            .GreaterThan(0).WithMessage("O ClienteId é obrigatório");
    }
}
