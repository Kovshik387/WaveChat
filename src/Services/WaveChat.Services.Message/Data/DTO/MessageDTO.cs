using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveChat.Services.Message.Data.DTO
{
    public class MessageDTO
    {
        public Guid Uid { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid UidUser { get; set; } = new Guid();
        public Guid UidChannel { get; set; } = new Guid();
        public DateTime SendDate { get; set; } = DateTime.Now;
        public string UrlPhoto { get; set; } = string.Empty;
    }
}
