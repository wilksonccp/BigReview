using AutoMapper;
using Banking.API.DTOs;
using Banking.API.Models;
using Banking.API.Services;
using Microsoft.AspNetCore.Mvc;
namespace Banking.API.Controllers;

public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IMapper _mapper;
    public ClienteController(IClienteService clienteService, IMapper mapper)
    {
        _clienteService = clienteService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> ListarTodosAsync()
    {
        var clientes = await _clienteService.ListarTodosAsync();
        return Ok(_mapper.Map<IEnumerable<ClienteDTO>>(clientes));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorIdAsync(Guid id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ClienteDTO>(cliente));
    }
    [HttpPost]
    public async Task<IActionResult> AdicionarAsync([FromBody] ClienteDTO clienteDto)
    {
        var cliente = _mapper.Map<Cliente>(clienteDto);
        var novoCliente = await _clienteService.AdicionarAsync(cliente);
        var clienteDTO = _mapper.Map<ClienteDTO>(novoCliente);

        return CreatedAtAction(nameof(ObterPorIdAsync), new { id = clienteDTO.Id }, clienteDTO);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] ClienteDTO clienteDto)
    {
        if (clienteDto == null || id != clienteDto.Id)
        {
            return BadRequest("Dados inválidos.");
        }
        var cliente = _mapper.Map<Cliente>(clienteDto);
        var atualizado = await _clienteService.AtualizarAsync(id, cliente);
        if (atualizado == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ClienteDTO>(atualizado));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverAsync(Guid id)
    {
        var removido = await _clienteService.RemoverAsync(id);
        if (!removido)
        {
            return NotFound();
        }
        return NoContent();
    }
}
