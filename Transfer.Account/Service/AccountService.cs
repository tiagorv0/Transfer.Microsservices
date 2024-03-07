using Transfer.Account.Domain.DTO;
using Transfer.Account.Events;
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

    public async Task<AccountResponse> DepositAsync(Guid id, decimal amount, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetOneAsync(x => x.Id == id && x.Active, cancellationToken);

        if (account is null)
            return default;

        account.Deposit(amount);

        await _accountRepository.UpdateAsync(id, account, cancellationToken);

        return new AccountResponse(account);
    }

    public async Task<AccountResponse> WithdrawAsync(Guid id, decimal amount, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetOneAsync(x => x.Id == id && x.Active, cancellationToken);

        if (account is null)
            return default;

        account.Withdraw(amount);

        await _accountRepository.UpdateAsync(id, account, cancellationToken);

        return new AccountResponse(account);
    }

    public async Task<AccountResponse> DeactivateAccount(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetOneAsync(x => x.Id == id && x.Active, cancellationToken);

        if (account is null)
            return default;

        account.Deactivate();

        await _accountRepository.UpdateAsync(id, account, cancellationToken);

        return new AccountResponse(account);
    }

    public async Task<bool> HasBalanceToTransfer(Guid id, decimal amount, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetOneAsync(x => x.Id == id && x.Active, cancellationToken);

        if (account is null)
            return default;

        return account.HasBalanceToTransfer(amount);
    }

    public async Task TransferBetweenAccounts(TransferEvent transfer, CancellationToken cancellationToken = default)
    {
        var sender = await _accountRepository.GetOneAsync(x => x.TransferKey == transfer.SenderKey && x.Active, cancellationToken);

        sender.Withdraw(transfer.Amount);

        await _accountRepository.UpdateAsync(sender.Id, sender, cancellationToken);

        var receiver = await _accountRepository.GetOneAsync(x => x.TransferKey == transfer.ReceiverKey && x.Active, cancellationToken);

        receiver.Deposit(transfer.Amount);
    }

    public async Task<AccountResponse> GetOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetOneAsync(x => x.Id == id && x.Active, cancellationToken);

        if (account is null)
            return default;

        return new AccountResponse(account);
    }

    public async Task<IEnumerable<AccountResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var accounts = await _accountRepository.GetAllAsync(x => x.Active, cancellationToken);

        return accounts.Select(account => new AccountResponse(account));
    }
}
