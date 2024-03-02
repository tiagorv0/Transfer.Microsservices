using Transfer.Notification.Domain.Dto;
using Transfer.Notification.Events;
using Transfer.Notification.Infra;

namespace Transfer.Notification.Service;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;

    public NotificationService(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateNotification(SendNotificationEvent notificationEvent)
    {
        var notification = notificationEvent.ToModel();

        await _repository.AddAsync(notification);
    }

    public async Task ViewNotification(Guid id, CancellationToken ct = default)
    {
        var notification = await _repository.GetOneAsync(x => x.Id == id);

        notification.ViewedNotification();

        await _repository.UpdateAsync(id, notification, ct);
    }

    public async Task<IEnumerable<NotificationResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var notifications = await _repository.GetAllAsync(cancellationToken: cancellationToken);
        return notifications.Select(x => new NotificationResponse(x));
    }
}
