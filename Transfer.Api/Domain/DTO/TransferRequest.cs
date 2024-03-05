using System.ComponentModel.DataAnnotations;

namespace Transfer.Api.Domain.DTO;

public class TransferRequest
{
    [Required]
    public string SenderKey { get; set; }
    [Required]
    public string SenderName { get; set; }
    [Required]
    public string ReceiverKey { get; set; }
    [Required]
    public string ReceiverName { get; set; }
    [Range(0, double.MaxValue)]
    [Required]
    public decimal Amount { get; set; }
    public DateTime? ScheduleDate { get; set; }

    public Transfer ToSendTransfer()
    {
        var transfer = new Transfer();
        transfer.SendTransfer(SenderKey, SenderName, ReceiverKey, ReceiverName, Amount);
        return transfer;
    }

    public Transfer ToScheduleTransfer()
    {
        var transfer = new Transfer();
        transfer.ScheduleTransfer(SenderKey, SenderName, ReceiverKey, ReceiverName, Amount, ScheduleDate!.Value);
        return transfer;
    }
}
