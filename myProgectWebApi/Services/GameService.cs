
using myProgectWebApi.DAL.DTOs;
using myProgectWebApi.DAL.Entities;
using myProgectWebApi.DAL.Repositories;
using myProgectWebApi.Services;

namespace Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<Game>> CreateAsync(GameDto dto)
        {
            var game = new Game
            {
                Name = dto.Name,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate,
                Price = dto.Price
            };

            var created = await _repository.CreateAsync(game);

            return new ServiceResponse<Game>
            {
                Data = created,
                Message = "Game created succsesfull"
            };
        }

        public async Task<ServiceResponse<Game>> UpdateAsync(GameUpdateDto dto)
        {
            var updated = await _repository.UpdateAsync(new Game
            {
                Id = dto.Id,
                Name = dto.Name,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate,
                Price = dto.Price
            });

            if (updated == null)
                return new ServiceResponse<Game>
                {
                    Success = false,
                    Message = "Game not found"
                };

            return new ServiceResponse<Game>
            {
                Data = updated,
                Message = "Game Update"
            };
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Game not found",
                    Data = false
                };

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Game delete"
            };
        }

        public async Task<ServiceResponse<Game>> GetByIdAsync(int id)
        {
            var game = await _repository.GetByIdAsync(id);

            if (game == null)
                return new ServiceResponse<Game>
                {
                    Success = false,
                    Message = "Game not found"
                };

            return new ServiceResponse<Game> { Data = game };
        }

        public async Task<ServiceResponse<IEnumerable<Game>>> GetAllAsync()
        {
            var games = await _repository.GetAllAsync();

            return new ServiceResponse<IEnumerable<Game>> { Data = games };
        }

        public async Task<ServiceResponse<IEnumerable<Game>>> GetByGenreAsync(string genre)
        {
            var games = await _repository.GetByGenreAsync(genre);

            return new ServiceResponse<IEnumerable<Game>>
            {
                Data = games,
                Message = games.Any() ? "" : "Game there is nothing"
            };
        }
    }
}