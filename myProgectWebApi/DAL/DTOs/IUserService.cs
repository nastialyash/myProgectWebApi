

using myProgectWebApi.Services;

namespace myProgectWebApi.DAL.DTOs
{
    public interface IUserService
    {
        Task<AuthResponse> RegisterAsync(UserRegisterDto dto);
    }
}
