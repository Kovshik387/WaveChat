namespace WaveChat.Context.Settings;

public class DbSettings
{
    public DbType DatabaseType { get; private set; }
    public string ConnectionString { get; private set; } = string.Empty;
    public DbInitSettings? InitSettings { get; private set; } 
}

public class DbInitSettings
{
    public bool AddDemoData { get; private set; }
}
