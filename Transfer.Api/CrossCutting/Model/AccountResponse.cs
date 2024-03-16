namespace Transfer.Api.CrossCutting.Model;

public class AccountResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public string Name { get; set; }
    public string TransferKey { get; set; }
    public decimal Balance { get; set; }

    public bool HasBalanceToTransfer(decimal amount)
    {
        return Balance >= amount;
    }
}
