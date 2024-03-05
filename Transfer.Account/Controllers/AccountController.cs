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

    [HttpPut("update-account/{id}")]
    public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] AccountRequest request, CancellationToken cancellationToken = default)
    {
        await _service.UpdateAccountAsync(id, request, cancellationToken);
        return Ok();
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
