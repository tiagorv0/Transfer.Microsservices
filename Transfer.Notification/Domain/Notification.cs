using Transfer.Notification.Domain.Enum;

namespace Transfer.Notification.Domain;

public class Notification
{
    public Notification()
    {
        
    }

    public Notification(NotificationStatus status, string title, string message, bool wasViewed, DateTime? viewedIn)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Status = status;
        Title = title;
        Message = message;
        WasViewed = wasViewed;
        ViewedIn = viewedIn;
    }

    public void ViewedNotification()
    {
        WasViewed = true;
        ViewedIn = DateTime.Now;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public NotificationStatus Status { get; set; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public bool WasViewed { get; private set; }
    public DateTime? ViewedIn { get; private set; }
}
