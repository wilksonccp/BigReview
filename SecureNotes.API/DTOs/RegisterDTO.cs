namespace SecureNotes.API.DTOs;

public class RegisterDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Role { get; set; } = "User"; // Default role
}
// This class represents the data transfer object for user registration, including name, email, password, and role.