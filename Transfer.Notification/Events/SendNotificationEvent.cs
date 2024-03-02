using Transfer.Notification.Domain.Enum;

namespace Transfer.Notification.Events;

public class SendNotificationEvent
{
    public NotificationStatus Status { get; set; }
    public string SenderName { get; private set; }
    public string ReceiverName { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime? ScheduleDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Domain.Notification ToModel()
    {
        return new Domain.Notification(Status, GetTitle(), GetMessage(), false, null);
    }

    private string GetMessage()
    {
        var value = string.Format("{0:0,0.00}", Amount);
        return Status switch
        {
            NotificationStatus.Scheduled => $"Transferência de valor R${value} de {SenderName} agendada para {ScheduleDate.GetValueOrDefault().ToShortDateString()}",
            NotificationStatus.Completed => $"Transferência de valor R${value} de {SenderName} foi realizada em {CreatedAt.ToShortDateString()}",
            NotificationStatus.Canceled => $"Transferência de valor R${value} foi cancelada por {SenderName}",
            _ => throw new ArgumentException("Status Inválido")
        };
    }

    private string GetTitle()
    {
        return Status switch
        {
            NotificationStatus.Scheduled => "Transferência Agendada",
            NotificationStatus.Completed => "Transferência Recebida",
            NotificationStatus.Canceled => "Transferência Cancelada",
            _ => throw new ArgumentException("Status Inválido")
        };
    }
}
