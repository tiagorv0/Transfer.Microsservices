namespace Transfer.Account.Events;

public class TransferEvent
{
    public string SenderKey { get; set; }
    public string ReceiverKey { get; set; }
    public decimal Amount { get; set; }

}
