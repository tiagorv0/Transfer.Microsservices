using Transfer.Api.Domain.Enum;

namespace Transfer.Api.Event;

public class SendNotificationEvent
{
    public SendNotificationEvent(Domain.Transfer transfer)
    {
        Status = transfer.Status;
        SenderName = transfer.SenderName;
        ReceiverName = transfer.ReceiverName;
        Amount = transfer.Amount;
        ScheduleDate = transfer.ScheduleDate;
        CreatedAt = transfer.CreatedAt;
    }

    public TransferStatus Status { get; private set; }
    public string SenderName { get; private set; }
    public string ReceiverName { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime? ScheduleDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
}
