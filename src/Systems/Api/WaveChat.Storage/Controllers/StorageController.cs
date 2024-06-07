using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaveChat.Services.Logger;
using WaveChat.Services.Storage.Infrastructure;

namespace WaveChat.Storage.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[Controller]/[Action]")]
    public class StorageController(IAppLogger appLogger, IStorageService storageService) : ControllerBase
    {
        private readonly IAppLogger _logger = appLogger;
        private readonly IStorageService _storageService = storageService;
        /// <summary>
        /// Добавление изображение профиля пользователя
        /// </summary>
        /// <param name="userId">id пользователя</param>
        /// <param name="file">изображение</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutProfileFile(string userId, IFormFile file)
        {
           return Ok(await _storageService.AddProfileFileAsync(userId, file));
        }
        /// <summary>
        /// Получения файла по Id пользователя и его названию
        /// </summary>
        /// <param name="userId">id пользователя</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            return Ok(await _storageService.GetPresignedFileAsync(userId));
        }
    }
}
