using System.ComponentModel.DataAnnotations;

namespace Transfer.Account.Domain.DTO;

public class AccountRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string TransferKey { get; set; }
    [Range(0, double.MaxValue)]
    [Required]
    public decimal Balance { get; set; }

    public Account ToAccount()
    {
        var account = new Account();
        account.Create(Name, TransferKey, Balance);
        return account;
    }
}
