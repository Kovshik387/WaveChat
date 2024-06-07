using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveChat.Services.Message.Data.DTO
{
    public class UserDTO
    {
        public Guid Uid { get; set; }
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;
    }
}
