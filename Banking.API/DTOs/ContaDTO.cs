namespace Banking.API.DTOs;

public class ContaDTO
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public decimal Saldo { get; set; } = 0.0m;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public int ClienteId { get; set; }
}
