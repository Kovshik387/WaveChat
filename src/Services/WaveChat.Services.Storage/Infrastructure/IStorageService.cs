using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveChat.Services.Storage.Infrastructure
{
    /// <summary>
    /// Сервис бизнес-логики для предоставления доступа к пользовательским файлам
    /// </summary>
    public interface IStorageService
    {
        public Task<string> AddProfileFileAsync(string userId, IFormFile file);
        public Task<string> GetPresignedFileAsync(string userId);
    }
}
    