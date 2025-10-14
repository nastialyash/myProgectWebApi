using myProgectWebApi.DAL.DTOs;
using myProgectWebApi.DAL.Entities;


namespace myProgectWebApi.Services
{
    public interface IGameService
    {
        Task<ServiceResponse<GameDto>> CreateAsync(GameDto dto);
        Task<ServiceResponse<GameDto>> UpdateAsync(int id, GameUpdateDto dto);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
        Task<ServiceResponse<GameDto>> GetByIdAsync(int id);
        Task<ServiceResponse<List<GameDto>>> GetAllAsync();
        Task<ServiceResponse<List<GameDto>>> GetByGenreAsync(string genre);

    }
}
