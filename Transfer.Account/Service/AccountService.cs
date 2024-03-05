using Transfer.Account.Domain.DTO;
using Transfer.Account.Infra;

namespace Transfer.Account.Service;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountResponse> CreateAccountAsync(AccountRequest request, CancellationToken cancellationToken = default)
    {
        var account = request.ToAccount();

        await _accountRepository.AddAsync(account, cancellationToken);

        return new AccountResponse(account);
    }

    public async Task<AccountResponse> DepositAsync(Guid id, decimal amount)
    {
        var account = await _accountRepository.GetOneAsync(account => account.Id == id);

        if (account is null)
            return default;

        account.Deposit(amount);

        await _accountRepository.UpdateAsync(id, account);

        return new AccountResponse(account);
    }

    public async Task<AccountResponse> WithdrawAsync(Guid id, decimal amount)
    {
        var account = await _accountRepository.GetOneAsync(account => account.Id == id);

        if (account is null)
            return default;

        account.Withdraw(amount);

        await _accountRepository.UpdateAsync(id, account);

        return new AccountResponse(account);
    }

    public async Task<AccountResponse> DeactivateAccount(Guid id)
    {
        var account = await _accountRepository.GetOneAsync(account => account.Id == id);

        if (account is null)
            return default;

        account.Deactivate();

        await _accountRepository.UpdateAsync(id, account);

        return new AccountResponse(account);
    }

    public async Task<AccountResponse> GetOneAsync(Guid id)
    {
        var account = await _accountRepository.GetOneAsync(account => account.Id == id);

        if (account is null)
            return default;

        return new AccountResponse(account);
    }
}
