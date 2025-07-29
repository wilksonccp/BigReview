using Banking.API.DTOs;
using FluentValidation;
namespace Banking.API.Validations;

public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
{
    public ClienteDTOValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres.");
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve ser um endereço de email válido.");
        RuleFor(c => c.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve conter apenas números e ter entre 10 e 11 dígitos.");
        RuleFor(c => c.DataNascimento)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .LessThan(DateTime.UtcNow).WithMessage("A data de nascimento não pode ser no futuro.");
    }
}
// This validator checks that the ClienteDTO has a valid Nome, Email, Telefone, and DataNascimento.