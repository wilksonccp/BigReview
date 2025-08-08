using AutoMapper;
using Banking.API.Services;
using Microsoft.AspNetCore.Mvc;
using Banking.API.DTOs;
using static Banking.API.DTOs.ContaDTO;

[ApiController]
[Route("api/[controller]")]
public class ContaController : ControllerBase
{
    private readonly IContaService _service;
    private readonly IMapper _mapper;

    public ContaController(IContaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<ContaResponseDTO>> Post([FromBody] CriarContaDTO dto, CancellationToken ct)
    {
        var conta = await _service.CriarContaAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = conta.Id }, _mapper.Map<ContaResponseDTO>(conta));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ContaResponseDTO>> GetById(Guid id, CancellationToken ct)
    {
        var conta = await _service.ObterPorIdAsync(id, ct);
        return conta is null ? NotFound() : Ok(_mapper.Map<ContaResponseDTO>(conta));
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ContaResponseDTO>>> Get([FromQuery] ContasFiltroDTO filtro, CancellationToken ct)
    {
        var result = await _service.ListarAsync(filtro, ct);
        var dto = new PagedResult<ContaResponseDTO>(result.Items.Select(_mapper.Map<ContaResponseDTO>).ToList(), result.Total, result.Page, result.PageSize);
        return Ok(dto);
    }

    [HttpPost("{id:guid}/depositos")]
    public async Task<ActionResult<ContaResponseDTO>> Depositar(Guid id, [FromBody] OperacaoValorDTO dto, CancellationToken ct)
    {
        var conta = await _service.DepositarAsync(id, dto.Valor, ct);
        return Ok(_mapper.Map<ContaResponseDTO>(conta));
    }

    [HttpPost("{id:guid}/saques")]
    public async Task<ActionResult<ContaResponseDTO>> Sacar(Guid id, [FromBody] OperacaoValorDTO dto, CancellationToken ct)
    {
        var conta = await _service.SacarAsync(id, dto.Valor, ct);
        return Ok(_mapper.Map<ContaResponseDTO>(conta));
    }

    [HttpPost("transferencias")]
    public async Task<IActionResult> Transferir([FromBody] TransferenciaDTO dto, CancellationToken ct)
    {
        await _service.TransferirAsync(dto.ContaOrigemId, dto.ContaDestinoId, dto.Valor, ct);
        return NoContent();
    }

    [HttpPost("{id:guid}/bloqueios")]
    public async Task<ActionResult<ContaResponseDTO>> Bloquear(Guid id, CancellationToken ct)
    {
        var conta = await _service.BloquearAsync(id, ct);
        return Ok(_mapper.Map<ContaResponseDTO>(conta));
    }

    [HttpDelete("{id:guid}/bloqueios")]
    public async Task<ActionResult<ContaResponseDTO>> Desbloquear(Guid id, CancellationToken ct)
    {
        var conta = await _service.DesbloquearAsync(id, ct);
        return Ok(_mapper.Map<ContaResponseDTO>(conta));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Encerrar(Guid id, CancellationToken ct)
    {
        await _service.EncerrarAsync(id, ct);
        return NoContent();
    }

    [HttpGet("{id:guid}/extrato")]
    public async Task<ActionResult<PagedResult<MovimentoDTO>>> Extrato(Guid id, [FromQuery] ExtratoFiltroDTO f, CancellationToken ct)
    {
        var result = await _service.ExtratoAsync(id, f.De, f.Ate, f.Page, f.PageSize, ct);
        var dto = new PagedResult<MovimentoDTO>(result.Items.Select(_mapper.Map<MovimentoDTO>).ToList(), result.Total, result.Page, result.PageSize);
        return Ok(dto);
    }
}
