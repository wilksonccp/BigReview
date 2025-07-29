namespace Banking.API.Models;

public class Conta
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public decimal Saldo { get; set; } = 0.0m;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; }
}
// Compare this snippet from DTOs/ContaDTO.cs: