using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using Transfer.Notification.Domain.Options;

namespace Transfer.Notification.Infra;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<Domain.Notification> _notifications;

    public NotificationRepository(IOptions<DatabaseOptions> options)
    {
        var databaseOptions = options.Value;
        var client = new MongoClient(databaseOptions.ConnectionString);
        var database = client.GetDatabase(databaseOptions.DatabaseName);
        _notifications = database.GetCollection<Domain.Notification>("Notifications");
    }

    public async Task AddAsync(Domain.Notification notification, CancellationToken cancellationToken = default)
    {
        await _notifications.InsertOneAsync(notification, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, Domain.Notification notification, CancellationToken cancellationToken = default)
    {
        await _notifications.ReplaceOneAsync(notification => notification.Id == id, notification, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Domain.Notification>> GetAllAsync(Expression<Func<Domain.Notification, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        if (filter is not null)
            return await _notifications.Find(filter).ToListAsync(cancellationToken);

        return await _notifications.AsQueryable().ToListAsync(cancellationToken);
    }

    public async Task<Domain.Notification> GetOneAsync(Expression<Func<Domain.Notification, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        return await _notifications.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _notifications.DeleteOneAsync(notification => notification.Id == id, cancellationToken);
    }
}
