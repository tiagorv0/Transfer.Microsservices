using Refit;
using Transfer.Api.CrossCutting.Model;

namespace Transfer.Api.CrossCutting;

public interface ITransferAccountApi
{
    [Get("/api/account/{transferKey}")]
    Task<ApiResponse<AccountResponse>> GetAccountByTransferKey(string transferKey);
}
