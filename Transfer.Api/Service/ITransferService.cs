using Transfer.Api.Domain.DTO;

namespace Transfer.Api.Service;

public interface ITransferService
{
    Task<TransferResponse> CreateTransferAsync(TransferRequest request, CancellationToken cancellationToken = default);
    Task CancelTransferAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TransferResponse> ScheduleTransferAsync(TransferRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<TransferResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TransferResponse> GetOneAsync(Guid id, CancellationToken cancellationToken = default);
}
