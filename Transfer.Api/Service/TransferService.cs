﻿using Transfer.Api.CrossCutting;
using Transfer.Api.Domain.DTO;
using Transfer.Api.Event;
using Transfer.Api.Infra;

namespace Transfer.Api.Service;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _repository;
    private readonly IEventBus _eventBus;
    private readonly ITransferAccountApi _transferAccount;

    public TransferService(ITransferRepository repository, IEventBus eventBus, ITransferAccountApi transferAccount)
    {
        _repository = repository;
        _eventBus = eventBus;
        _transferAccount = transferAccount;
    }

    public async Task<TransferResponse> CreateTransferAsync(TransferRequest request, CancellationToken cancellationToken = default)
    {
        var (senderName, receiverName) = await VerifyBalanceAndGetSenderAndReceiverName(request);

        var transfer = request.ToSendTransfer(senderName, receiverName);

        await _repository.AddAsync(transfer, cancellationToken);

        _eventBus.Publish<SendNotificationEvent>(new(transfer), "notification");

        _eventBus.Publish<RealizeTransferAccountEvent>(new(transfer), "account");

        return new TransferResponse(transfer);
    }

    public async Task<TransferResponse> CancelTransferAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var transfer = await _repository.GetOneAsync(x => x.Id == id && x.Status == Domain.Enum.TransferStatus.Scheduled, cancellationToken);

        if (transfer is null)
            return default;

        transfer.Cancel();

        await _repository.UpdateAsync(id, transfer, cancellationToken);

        _eventBus.Publish<SendNotificationEvent>(new(transfer), "notification");

        return new TransferResponse(transfer);
    }

    public async Task<TransferResponse> ScheduleTransferAsync(TransferRequest request, CancellationToken cancellationToken = default)
    {
        var (senderName, receiverName) = await VerifyBalanceAndGetSenderAndReceiverName(request);

        var transfer = request.ToScheduleTransfer(senderName, receiverName);

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

    private async Task<(string senderName, string receiverName)> VerifyBalanceAndGetSenderAndReceiverName(TransferRequest request)
    {
        var accountSender = await _transferAccount.GetAccountByTransferKey(request.SenderKey);
        var accountReceiver = await _transferAccount.GetAccountByTransferKey(request.ReceiverKey);

        if (!accountSender.IsSuccessStatusCode)
            throw new ArgumentException("Sender not found");

        if (!accountReceiver.IsSuccessStatusCode)
            throw new ArgumentException("Receiver not found");


        if (accountSender.Content.HasBalanceToTransfer(request.Amount))
            return (accountSender.Content.Name, accountReceiver.Content.Name);

        throw new ArgumentException("Insufficient balance");
    }
}
