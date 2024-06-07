using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveChat.Services.Message.Data.DTO
{
    public class ChatDTO
    {
        public Guid Uid { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? LastMessage { get; set; } = string.Empty;
        public bool MyMessage { get; set; } = false;
        public IList<UserDTO> Users { get; set; } = null!;
    }
}
