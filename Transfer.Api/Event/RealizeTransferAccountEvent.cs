namespace Transfer.Api.Event;

public class RealizeTransferAccountEvent
{
    public string SenderKey { get; private set; }
    public string ReceiverKey { get; private set; }
    public decimal Amount { get; private set; }

    public RealizeTransferAccountEvent(Domain.Transfer transfer)
    {
        SenderKey = transfer.SenderKey;
        ReceiverKey = transfer.ReceiverKey;
        Amount = transfer.Amount;
    }

}
