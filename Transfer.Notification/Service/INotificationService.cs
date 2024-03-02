using Transfer.Notification.Domain.Dto;
using Transfer.Notification.Events;

namespace Transfer.Notification.Service;

public interface INotificationService
{
    Task CreateNotification(SendNotificationEvent notificationEvent);
    Task ViewNotification(Guid id, CancellationToken ct = default);
    Task<IEnumerable<NotificationResponse>> GetAllAsync(CancellationToken cancellationToken = default);
}
