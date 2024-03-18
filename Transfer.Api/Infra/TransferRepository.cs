using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using Transfer.Api.Domain.Options;
namespace Transfer.Api.Infra;

public class TransferRepository : ITransferRepository
{
    private readonly IMongoCollection<Domain.Transfer> _transfers;

    public TransferRepository(IOptions<DatabaseOptions> options)
    {
        var databaseOptions = options.Value;
        var client = new MongoClient(databaseOptions.ConnectionString);
        var database = client.GetDatabase(databaseOptions.DatabaseName);
        _transfers = database.GetCollection<Domain.Transfer>("Transfers");
    }

    public async Task AddAsync(Domain.Transfer transfer, CancellationToken cancellationToken = default)
    {
        await _transfers.InsertOneAsync(transfer, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, Domain.Transfer transfer, CancellationToken cancellationToken = default)
    {
        await _transfers.ReplaceOneAsync(transfer => transfer.Id == id, transfer, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Domain.Transfer>> GetAllAsync(Expression<Func<Domain.Transfer, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        if (filter is not null)
            return await _transfers.Find(filter).ToListAsync(cancellationToken);

        return await _transfers.AsQueryable().ToListAsync(cancellationToken);
    }

    public async Task<Domain.Transfer> GetOneAsync(Expression<Func<Domain.Transfer, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        return await _transfers.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _transfers.DeleteOneAsync(transfer => transfer.Id == id, cancellationToken);
    }
}
