using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureNotes.API.Models;
using System.Security.Claims;

namespace SecureNotes.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize] // todas as rotas exigem autenticação
public class NotesController : ControllerBase
{
    // Simulando um banco de dados em memória
    private static List<Nota> _notas = new();
    private static int _idCounter = 1;
    private readonly ILogger<NotesController> _logger;

    public NotesController(ILogger<NotesController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public IActionResult ObterNotas()
    {
        foreach (var claim in User.Claims)
        {
            _logger.LogDebug("Claim: {Type} = {Value}", claim.Type, claim.Value);
        }
        // ✅ Usa ClaimTypes para buscar o ID com segurança
        var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (usuarioIdClaim is null)
            return Unauthorized("Claim 'NameIdentifier' não encontrada.");

        var usuarioId = int.Parse(usuarioIdClaim.Value);

        var notasDoUsuario = _notas
            .Where(n => n.UsuarioId == usuarioId)
            .ToList();

        _logger.LogInformation("Usuário {UserId} está consultando suas notas", usuarioId);
        return Ok(notasDoUsuario);
    }

    [HttpPost]
    public IActionResult CriarNota([FromBody] string conteudo)
    {
        foreach (var claim in User.Claims)
        {
            _logger.LogDebug("Claim: {Type} = {Value}", claim.Type, claim.Value);
        }
        // ✅ Mesmo processo aqui: valida claim antes de usar
        var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (usuarioIdClaim is null)
            return Unauthorized("Claim 'NameIdentifier' não encontrada.");

        var usuarioId = int.Parse(usuarioIdClaim.Value);

        var novaNota = new Nota
        {
            Id = _idCounter++,
            UsuarioId = usuarioId,
            Conteudo = conteudo
        };

        _notas.Add(novaNota);

        _logger.LogInformation("Usuário {UserId} está criando uma nova nota: {Conteudo}", usuarioId, conteudo);
        return CreatedAtAction(nameof(ObterNotas), new { id = novaNota.Id }, novaNota);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // 🔐 Apenas usuários com role "Admin" podem deletar
    public IActionResult DeletarNota(int id)
    {
        foreach (var claim in User.Claims)
        {
            _logger.LogDebug("Claim: {Type} = {Value}", claim.Type, claim.Value);
        }
        var nota = _notas.FirstOrDefault(n => n.Id == id);
        _logger.LogWarning("Usuário {User} (possivelmente Admin) está tentando deletar a nota com ID {NotaId}",
           User.Identity?.Name ?? "desconhecido", id);
        if (nota is null)
            return NotFound();

        _notas.Remove(nota);
        _logger.LogInformation("Nota {NotaId} deletada com sucesso", id);

        return NoContent();
    }
}
