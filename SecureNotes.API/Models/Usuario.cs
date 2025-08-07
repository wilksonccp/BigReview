namespace SecureNotes.API.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public string Role { get; set; } = "User"; // Default role
}
// This class represents a user in the system with properties for ID, name, email, password hash, creation date, and role.