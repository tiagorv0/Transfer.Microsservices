namespace Transfer.Api.Domain.DTO;

public class TransferRequest
{
    public string SenderKey { get; set; }
    public string SenderName { get; set; }
    public Guid SenderId { get; set; }
    public string ReceiverKey { get; set; }
    public string ReceiverName { get; set; }
    public Guid ReceiverId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public DateTime? ScheduleDate { get; set; }

    public Transfer ToSendTransfer()
    {
        var transfer = new Transfer();
        transfer.SendTransfer(SenderKey, SenderName, ReceiverKey, ReceiverName, Amount, SenderId, ReceiverId);
        return transfer;
    }

    public Transfer ToScheduleTransfer()
    {
        var transfer = new Transfer();
        transfer.ScheduleTransfer(SenderKey, SenderName, ReceiverKey, ReceiverName, Amount, ScheduleDate!.Value, SenderId, ReceiverId);
        return transfer;
    }
}
