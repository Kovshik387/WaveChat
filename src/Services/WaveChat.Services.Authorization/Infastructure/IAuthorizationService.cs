using WaveChat.Services.Authorization.Data.DTO;
using WaveChat.Services.Authorization.Data.Responses;

namespace WaveChat.Services.Authorization.Infastructure;

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
    public Task<AuthResponse<AuthDTO>> SignInAsync(SignInDTO model);
    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="signUp"></param>
    /// <returns></returns>
    public Task<AuthResponse<AuthDTO>> SignUpAsync(SignUpDTO model);
    /// <summary>
    /// Проверка на наличие пользователей в БД 
    /// </summary>
    /// <returns></returns>
    Task<bool> IsEmptyAsync();
    /// <summary>
    /// Выдача нового access-токена
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    public Task<AuthResponse<AuthDTO>> GetAccessTokenAsync(string refreshToken);
    /// <summary>
    /// Удаление refresh-токена у пользователя
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    public Task<AuthResponse<object>> Logout(string refreshToken);
}
