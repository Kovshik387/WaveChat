using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Services.Authorization.Data.DTO;
using WaveChat.Services.Authorization.Data.Responses;

namespace WaveChat.Services.Authorization.Infastructure
{
    /// <summary>
    /// Сервис бизнес-логики для авторизации и регистрации
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Аутентификация в существующий аккаунт
        /// </summary>
        /// <param name="signIn"></param>
        /// <returns></returns>
        Task<AuthorizationResponse<AuthorizationDTO>> SignInAsync(SignInDTO signIn);
        
    }
}
