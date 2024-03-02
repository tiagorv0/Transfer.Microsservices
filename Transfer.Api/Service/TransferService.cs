using Transfer.Api.Domain.DTO;
using Transfer.Api.Event;
using Transfer.Api.Infra;

namespace Transfer.Api.Service;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _repository;
    private readonly IEventBus _eventBus;

    public TransferService(ITransferRepository repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<TransferResponse> CreateTransferAsync(TransferRequest request, CancellationToken cancellationToken = default)
    {
        var transfer = request.ToSendTransfer();

        await _repository.AddAsync(transfer, cancellationToken);

        _eventBus.Publish<SendNotificationEvent>(new(transfer), "notification");

        return new TransferResponse(transfer);
    }

    public async Task CancelTransferAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var transfer = await _repository.GetOneAsync(x => x.Id == id, cancellationToken);

        transfer.Cancel();

        await _repository.UpdateAsync(id, transfer, cancellationToken);

        _eventBus.Publish<SendNotificationEvent>(new(transfer), "notification");
    }

    public async Task<TransferResponse> ScheduleTransferAsync(TransferRequest request, CancellationToken cancellationToken = default)
    {
        var transfer = request.ToScheduleTransfer();

        await _repository.AddAsync(transfer, cancellationToken);

        _eventBus.Publish<SendNotificationEvent>(new(transfer), "notification");

        return new TransferResponse(transfer);
    }

    public async Task<IEnumerable<TransferResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var transfers = await _repository.GetAllAsync(cancellationToken: cancellationToken);
        return transfers.Select(x => new TransferResponse(x));
    }

    public async Task<TransferResponse> GetOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var transfer = await _repository.GetOneAsync(x => x.Id == id, cancellationToken);

        if (transfer is null)
            return default;

        return new TransferResponse(transfer);
    }
}
