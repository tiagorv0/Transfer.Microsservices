using Microsoft.AspNetCore.Mvc;
using Transfer.Api.Domain.DTO;
using Transfer.Api.Service;

namespace Transfer.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class TransferController : ControllerBase
{
    private readonly ITransferService _service;

    public TransferController(ITransferService service)
    {
        _service = service;
    }

    [HttpPost("create-transfer")]
    public async Task<IActionResult> CreateTransfer([FromBody] TransferRequest request, CancellationToken cancellationToken = default)
    {
        return Ok(await _service.CreateTransferAsync(request, cancellationToken));
    }

    [HttpPost("schedule-transfer")]
    public async Task<IActionResult> ScheduleTransfer([FromBody] TransferRequest request, CancellationToken cancellationToken = default)
    {
        return Ok(await _service.ScheduleTransferAsync(request, cancellationToken));
    }

    [HttpPut("cancel-transfer/{id}")]
    public async Task<IActionResult> CancelTransfer(Guid id, CancellationToken cancellationToken = default)
    {
        var transfer = await _service.CancelTransferAsync(id, cancellationToken);

        if (transfer is null)
            return NotFound();

        return Ok(transfer);
    }

    [HttpGet]
    public async Task<IActionResult> GetTransfers(CancellationToken cancellationToken = default)
    {
        return Ok(await _service.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransferById(Guid id, CancellationToken cancellationToken = default)
    {
        var transfer = await _service.GetOneAsync(id, cancellationToken);

        if (transfer is null)
            return NotFound();

        return Ok(transfer);
    }


}
