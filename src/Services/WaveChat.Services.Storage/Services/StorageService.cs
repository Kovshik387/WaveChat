using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using WaveChat.Context;
using WaveChat.Services.Storage.Infrastructure;

namespace WaveChat.Services.Storage.Services
{
    /// <summary>
    /// Реализация интерфейса <see cref="IStorageService"/> хранения
    /// </summary>
    public class StorageService(ILogger<StorageService> logger, IMinioClientFactory minioClientFactory, CorporateMessengerContext context) : IStorageService
    {
        private readonly ILogger<StorageService> _logger = logger;
        private readonly CorporateMessengerContext _context = context;
        private readonly IMinioClientFactory _minioClientFactory = minioClientFactory;
        public async Task<bool> AddProfileFileAsync(string userId, IFormFile file)
        {
            try
            {
                using (var minioClient = _minioClientFactory.CreateClient())
                {

                    if (!await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(userId)))
                    {
                        await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(userId));
                        _logger.LogInformation("Create bucket");
                    }

                    var fileName = Guid.NewGuid().ToString() + ".png";

                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Uid.ToString() == userId);

                    var putObjectArgs = new PutObjectArgs()
                        .WithBucket(userId)
                        .WithStreamData(file.OpenReadStream())
                        .WithObject(fileName)
                        .WithContentType(file.ContentType)
                        .WithObjectSize(file.Length)
                    ;  

                    _context.Photos.Add(new Context.Entities.Photo()
                    {
                        Bucket = userId,
                        Image = fileName,
                        IduserNavigation = user,
                        IsProfile = true,
                        IsActive = true,
                    });
                    await _context.SaveChangesAsync();

                    await minioClient.PutObjectAsync(putObjectArgs);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<string> GetPresignedFileAsync(string userId)
        {
            using (var minioClient = _minioClientFactory.CreateClient())
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Uid == Guid.Parse(userId));
                var fileName = await _context.Photos.FirstOrDefaultAsync(x => x.IsProfile == true && x.IsActive == true);

                if (fileName is null) throw new ArgumentNullException();

                var args = new PresignedGetObjectArgs()
                    .WithBucket(userId)
                    .WithObject(fileName!.Image)
                    .WithExpiry(60*60*60);
                return await minioClient.PresignedGetObjectAsync(args);
            }
        }
    }
}
