using Transfer.Notification.Domain.Enum;

namespace Transfer.Notification.Domain.Dto;

public class NotificationResponse
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public NotificationStatus Status { get; set; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public bool WasViewed { get; private set; }

    public NotificationResponse(Notification notification)
    {
        Id = notification.Id;
        CreatedAt = notification.CreatedAt;
        Status = notification.Status;
        Title = notification.Title;
        Message = notification.Message;
        WasViewed = notification.WasViewed;
    }
}
