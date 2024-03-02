using System.Linq.Expressions;

namespace Transfer.Api.Infra;

public interface ITransferRepository
{
    Task AddAsync(Domain.Transfer transfer, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, Domain.Transfer transfer, CancellationToken cancellationToken = default);
    Task<IEnumerable<Domain.Transfer>> GetAllAsync(Expression<Func<Domain.Transfer, bool>> filter = null, CancellationToken cancellationToken = default);
    Task<Domain.Transfer> GetOneAsync(Expression<Func<Domain.Transfer, bool>> filter = null, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
