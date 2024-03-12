namespace WaveChat.Identity.Configuration;

using WaveChat.Common.Security;
using Duende.IdentityServer.Models;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.BooksRead, "Read"),
            new ApiScope(AppScopes.BooksWrite, "Write")
        };
}