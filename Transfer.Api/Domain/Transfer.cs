using Transfer.Api.Domain.Enum;

namespace Transfer.Api.Domain;

public class Transfer
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public string SenderKey { get; private set; }
    public string SenderName { get; private set; }
    public string ReceiverKey { get; private set; }
    public string ReceiverName { get; private set; }
    public TransferStatus Status { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime? ScheduleDate { get; private set; }
    

    public void Cancel()
    {
        if (!ScheduleDate.HasValue)
            throw new InvalidOperationException("Transfer can't be canceled");

        UpdatedAt = DateTime.Now;
        Status = TransferStatus.Canceled;
    }

    public void SendTransfer(string senderKey, string senderName, string receiverKey, string receiverName, decimal amount)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        SenderKey = senderKey;
        SenderName = senderName;
        ReceiverKey = receiverKey;
        ReceiverName = receiverName;
        Amount = amount;
        Status = TransferStatus.Completed;
    }

    public void ScheduleTransfer(string senderKey, string senderName, string receiverKey, string receiverName, decimal amount, DateTime scheduleDate)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        SenderKey = senderKey;
        SenderName = senderName;
        ReceiverKey = receiverKey;
        ReceiverName = receiverName;
        Amount = amount;
        ScheduleDate = scheduleDate;
        Status = TransferStatus.Scheduled;
    }
}
