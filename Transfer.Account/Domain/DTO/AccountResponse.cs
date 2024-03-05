namespace Transfer.Account.Domain.DTO;

public class AccountResponse
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdateAt { get; private set; }
    public string Name { get; private set; }
    public string TransferKey { get; private set; }
    public decimal Balance { get; private set; }

    public AccountResponse(Account account)
    {
        Id = account.Id;
        CreatedAt = account.CreatedAt;
        UpdateAt = account.UpdateAt;
        Name = account.Name;
        TransferKey = account.TransferKey;
        Balance = account.Balance;
    }
}
