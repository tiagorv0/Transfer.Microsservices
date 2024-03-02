using System.Linq.Expressions;

namespace Transfer.Account.Infra;

public interface IAccountRepository
{
    Task AddAsync(Domain.Account account, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, Domain.Account account, CancellationToken cancellationToken = default);
    Task<IEnumerable<Domain.Account>> GetAllAsync(Expression<Func<Domain.Account, bool>> filter = null, CancellationToken cancellationToken = default);
    Task<Domain.Account> GetOneAsync(Expression<Func<Domain.Account, bool>> filter = null, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
