using Azure.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveChat.Services.Authorization.Infastructure
{
    public interface IConnectionWithStorageApi
    {
        public Task<bool> AttachUserProfile(IFormFile file, string userId,string accessToken);
    }
}
