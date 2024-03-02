using Transfer.Api.Domain.Enum;

namespace Transfer.Api.Domain.DTO;

public class TransferResponse
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string SenderKey { get; private set; }
    public string SenderName { get; private set; }
    public string ReceiverKey { get; private set; }
    public string ReceiverName { get; private set; }
    public decimal Amount { get; private set; }
    public TransferStatus Status { get; set; }
    public DateTime? ScheduleDate { get; private set; }

    public TransferResponse(Transfer transfer)
    {
        Id = transfer.Id;
        CreatedAt = transfer.CreatedAt;
        SenderKey = transfer.SenderKey;
        SenderName = transfer.SenderName;
        ReceiverKey = transfer.ReceiverKey;
        ReceiverName = transfer.ReceiverName;
        Amount = transfer.Amount;
        Status = transfer.Status;
        ScheduleDate = transfer.ScheduleDate;
    }
}
