using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveChat.Services.Account.Data.DTO
{
    public class AccountDto
    {
        public string Uid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname {  get; set; } = string.Empty;
        public string LastName {  get; set; } = string.Empty;
        public string UserName {  get; set; } = string.Empty;
        public string UrlImage {  get; set; } = string.Empty;
    }
}
