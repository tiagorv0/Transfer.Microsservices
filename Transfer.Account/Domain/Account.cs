namespace Transfer.Account.Domain;

public class Account
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdateAt { get; private set; }
    public string Name { get; private set; }
    public string TransferKey { get; private set; }
    public decimal Balance { get; private set; }
    public bool Active { get; private set; }

    public void Create(string name, string transferKey, decimal balance)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Name = name;
        TransferKey = transferKey;
        Balance = balance;
        Active = true;
    }

    public void Update(string name)
    {
        UpdateAt = DateTime.Now;
        Name = name;
    }

    public void Deactivate()
    {
        UpdateAt = DateTime.Now;
        Active = false;
    }

    public void Deposit(decimal amount)
    {
        UpdateAt = DateTime.Now;
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        UpdateAt = DateTime.Now;
        Balance -= amount;

        if (Balance < 0)
            throw new InvalidOperationException("Insufficient funds");
    }

    public bool HasBalanceToTransfer(decimal amount)
    {
        return Balance >= amount;
    }
}
