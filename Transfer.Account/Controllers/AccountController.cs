using Microsoft.AspNetCore.Mvc;
using Transfer.Account.Domain.DTO;
using Transfer.Account.Service;

namespace Transfer.Account.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost("create-account")]
    public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request, CancellationToken cancellationToken = default)
    {
        return Ok(await _service.CreateAccountAsync(request, cancellationToken));
    }

    [HttpPut("withdraw/{id}")]
    public async Task<IActionResult> Withdraw(Guid id, decimal amount, CancellationToken cancellationToken = default)
    {
        var result = await _service.WithdrawAsync(id, amount, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("deposit/{id}")]
    public async Task<IActionResult> Deposit(Guid id, decimal amount, CancellationToken cancellationToken = default)
    {
        var result = await _service.DepositAsync(id, amount, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("deactivate/{id}")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _service.DeactivateAccount(id, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("hasBalance-to-transfer/{id}")]
    public async Task<IActionResult> HasBalanceToTransfer(Guid id, decimal amount, CancellationToken cancellationToken = default)
    {
        var account = await _service.HasBalanceToTransfer(id, amount, cancellationToken);

        return Ok(account);
    }

    [HttpGet]
    public async Task<IActionResult> GetAccounts(CancellationToken cancellationToken = default)
    {
        return Ok(await _service.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountById(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await _service.GetOneAsync(id, cancellationToken);

        if (account is null)
            return NotFound();

        return Ok(account);
    }
}
