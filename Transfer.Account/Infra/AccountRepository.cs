using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using Transfer.Account.Domain.Options;

namespace Transfer.Account.Infra;

public class AccountRepository : IAccountRepository
{
    private readonly IMongoCollection<Domain.Account> _accounts;

    public AccountRepository(IOptions<DatabaseOptions> options)
    {
        var databaseOptions = options.Value;
        var client = new MongoClient(databaseOptions.ConnectionString);
        var database = client.GetDatabase(databaseOptions.DatabaseName);
        _accounts = database.GetCollection<Domain.Account>("Accounts");
    }

    public async Task AddAsync(Domain.Account account, CancellationToken cancellationToken = default)
    {
        await _accounts.InsertOneAsync(account, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, Domain.Account account, CancellationToken cancellationToken = default)
    {
        await _accounts.ReplaceOneAsync(account => account.Id == id, account, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Domain.Account>> GetAllAsync(Expression<Func<Domain.Account, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        return await _accounts.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<Domain.Account> GetOneAsync(Expression<Func<Domain.Account, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        return await _accounts.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _accounts.DeleteOneAsync(account => account.Id == id, cancellationToken);
    }
}
