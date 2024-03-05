using Transfer.Account.Domain.DTO;

namespace Transfer.Account.Service;

public interface IAccountService
{
    Task<AccountResponse> CreateAccountAsync(AccountRequest request, CancellationToken cancellationToken = default);
}
