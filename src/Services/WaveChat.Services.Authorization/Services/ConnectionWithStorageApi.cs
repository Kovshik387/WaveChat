using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Services.Authorization.Infastructure;
using WaveChat.Services.Settings.Settings;

namespace WaveChat.Services.Authorization.Services
{
    public class ConnectionWithStorageApi(PathSettings pathSettings,ILogger<ConnectionWithStorageApi> logger) : IConnectionWithStorageApi
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly PathSettings _pathSettings = pathSettings;
        private readonly ILogger<ConnectionWithStorageApi> _logger = logger;
        public async Task<bool> AttachUserProfile(IFormFile file, string userId,string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
            var uri = new Uri(_pathSettings.AddStoragePath);

            _logger.LogInformation(uri.ToString());

            return true;
        }
    }
}
