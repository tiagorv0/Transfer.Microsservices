using Transfer.Account.Domain.DTO;
using Transfer.Account.Events;

namespace Transfer.Account.Service;

public interface IAccountService
{
    Task<AccountResponse> CreateAccountAsync(AccountRequest request, CancellationToken cancellationToken = default);
    Task<AccountResponse> DepositAsync(Guid id, decimal amount, CancellationToken cancellationToken = default);
    Task<AccountResponse> WithdrawAsync(Guid id, decimal amount, CancellationToken cancellationToken = default);
    Task<AccountResponse> DeactivateAccount(Guid id, CancellationToken cancellationToken = default);
    Task<AccountResponse> ReactivateAccount(Guid id, CancellationToken cancellationToken = default);
    Task<bool> HasBalanceToTransfer(string transferKey, decimal amount, CancellationToken cancellationToken = default);
    Task<AccountResponse> GetOneAsync(string transferKey, CancellationToken cancellationToken = default);
    Task<IEnumerable<AccountResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task TransferBetweenAccounts(TransferEvent transfer, CancellationToken cancellationToken = default);
}
