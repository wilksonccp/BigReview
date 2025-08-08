namespace Banking.API.Models.entities;

public class Cliente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public List<Conta> Contas { get; set; } = new();
}
// Compare this snippet from Models/Conta.cs: