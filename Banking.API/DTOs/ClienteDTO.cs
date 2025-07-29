namespace Banking.API.DTOs;

public class ClienteDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public ICollection<ContaDTO> Contas { get; set; } = new List<ContaDTO>();
}
// This DTO represents a client in the banking system, including their personal information and associated accounts.