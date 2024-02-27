namespace WaveChat.Context;

using Microsoft.EntityFrameworkCore;
using WaveChat.Context;

public class DbContextFactory
{
    private readonly DbContextOptions<CorporateMessengerContext> options;

    public DbContextFactory(DbContextOptions<CorporateMessengerContext> options)
    {
        this.options = options;
    }

    public CorporateMessengerContext Create()
    {
        return new CorporateMessengerContext(options);
    }
}