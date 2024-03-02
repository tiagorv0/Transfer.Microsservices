using System.Linq.Expressions;

namespace Transfer.Notification.Infra;

public interface INotificationRepository
{
    Task AddAsync(Domain.Notification notification, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, Domain.Notification notification, CancellationToken cancellationToken = default);
    Task<IEnumerable<Domain.Notification>> GetAllAsync(Expression<Func<Domain.Notification, bool>> filter = null, CancellationToken cancellationToken = default);
    Task<Domain.Notification> GetOneAsync(Expression<Func<Domain.Notification, bool>> filter = null, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
