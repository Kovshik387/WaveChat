using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveChat.Services.Settings.Settings
{
    public class MinioSettings
    {
        /// <summary>
        /// Подключение к Minio
        /// </summary>
        public string EndPoint { get; set; } = string.Empty;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string AccessKey {  get; set; } = string.Empty;
       /// <summary>
       /// Секрет 
       /// </summary>
        public string SecretKey { get; set; } = string.Empty;
        /// <summary>
        /// Включение SSL подключения
        /// </summary>
        public bool Ssl {  get; set; } = false;
    }
}
