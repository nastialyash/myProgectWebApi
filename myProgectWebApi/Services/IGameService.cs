using myProgectWebApi.DAL.DTOs;
using myProgectWebApi.DAL.Entities;


namespace myProgectWebApi.Services
{
    public interface IGameService
    {
        Task<ServiceResponse<Game>> CreateAsync(GameDto dto);
        Task<ServiceResponse<Game>> UpdateAsync(GameUpdateDto dto);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
        Task<ServiceResponse<Game>> GetByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<Game>>> GetAllAsync();
        Task<ServiceResponse<IEnumerable<Game>>> GetByGenreAsync(string genre);

    }
}
