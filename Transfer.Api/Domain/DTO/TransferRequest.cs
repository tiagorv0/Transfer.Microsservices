using System.ComponentModel.DataAnnotations;

namespace Transfer.Api.Domain.DTO;

public class TransferRequest
{
    [Required]
    public string SenderKey { get; set; }
    [Required]
    public string ReceiverKey { get; set; }
    [Range(0, double.MaxValue)]
    [Required]
    public decimal Amount { get; set; }
    public DateTime? ScheduleDate { get; set; }

    public Transfer ToSendTransfer(string senderName, string receiverName)
    {
        var transfer = new Transfer();
        transfer.SendTransfer(SenderKey, senderName, ReceiverKey, receiverName, Amount);
        return transfer;
    }

    public Transfer ToScheduleTransfer(string senderName, string receiverName)
    {
        var transfer = new Transfer();
        transfer.ScheduleTransfer(SenderKey, senderName, ReceiverKey, receiverName, Amount, ScheduleDate!.Value);
        return transfer;
    }
}
