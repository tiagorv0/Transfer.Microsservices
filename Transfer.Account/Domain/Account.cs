namespace Transfer.Account.Domain;

public class Account
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdateAt { get; set; }
    public string Name { get; private set; }
    public string TransferKey { get; private set; }
    public decimal Balance { get; private set; }

}
