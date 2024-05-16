namespace WaveChat.Storage.Configuration;

using Minio;
using System.Net;
using WaveChat.Services.Settings.Settings;

public static class MinioConfiguration
{
    public static IServiceCollection AddAppMinio(this IServiceCollection services,MinioSettings minioSettings)
    {
        services.AddMinio(x => x
            .WithEndpoint(minioSettings.EndPoint)
            .WithProxy(new WebProxy("minio",9000))
            .WithCredentials(minioSettings.AccessKey,minioSettings.SecretKey)
            .WithSSL(minioSettings.Ssl)
        );
        return services;
    }
}
