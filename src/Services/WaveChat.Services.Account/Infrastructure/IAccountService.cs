using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Services.Account.Data.DTO;

namespace WaveChat.Services.Account.Infrastructure;

public interface IAccountService
{
    public Task<bool> UpdateAccountDataAsync(AccountDetailsDTO account);
    public Task<AccountDto> GetAccountByIdAsync(string id);
    public Task<AccountDetailsDTO> GetAccountDetailsByIdAsync(string id);
    public Task<List<AccountDto>> GetAccountByUserNameAsync(string name, string id);
}
