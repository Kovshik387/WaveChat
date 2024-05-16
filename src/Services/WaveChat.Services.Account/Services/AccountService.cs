using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WaveChat.Common;
using WaveChat.Common.Extensions;
using WaveChat.Context;
using WaveChat.Services.Account.Data.DTO;
using WaveChat.Services.Account.Infrastructure;

namespace WaveChat.Services.Account.Services;

public class AccountService(CorporateMessengerContext context,IMapper mapper, ILogger<AccountService> logger) : IAccountService
{
    private readonly CorporateMessengerContext _context = context;
    private readonly HttpClient _httpClient = new();
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<AccountService> _logger = logger;
    public async Task<AccountDto> GetAccountByIdAsync(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Uid.Equals(Guid.Parse(id)));

        if (user is null) throw new Exception();

        var accountDto = _mapper.Map<AccountDto>(user);

        var url = new Uri($"http://storage_service:8080/v1/Storage/GetUserProfile?userId={accountDto.Uid}");

        using var request = new HttpRequestMessage(HttpMethod.Get, url.ToString());
        
        using var response = await _httpClient.SendAsync(request);
        accountDto.UrlImage = await response.Content.ReadAsStringAsync();
        
        return accountDto;
    }

    public async Task<bool> UpdateAccountDataAsync(AccountDto account)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Uid.Equals(account.Uid));

        if (user is null) return false;

        user.Surname = account.Surname; user.Username = account.UserName;
        user.Name = account.Name; user.Lastname = account.LastName;

        _context.Users.Update(user); await _context.SaveChangesAsync();
        
        return true;
    }
}
