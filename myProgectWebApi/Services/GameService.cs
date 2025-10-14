
using myProgectWebApi.DAL.DTOs;
using myProgectWebApi.DAL.Entities;
using myProgectWebApi.DAL.Repositories;
using myProgectWebApi.Services;

namespace Services
{

    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        private readonly IWebHostEnvironment _env;

        public GameService(IGameRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }


       public  async Task<ServiceResponse<GameDto>> CreateAsync(GameDto dto)
        {
            var response = new ServiceResponse<GameDto>();

            try
            {
                var game = new Game
                {
                    Title = dto.Title,
                    Genre = dto.Genre,
                    Description = dto.Description,
                    Price = dto.Price
                };

                await _repository.CreateAsync(game);


                string folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "games", game.Id.ToString());
                Directory.CreateDirectory(folderPath);

                response.Data = dto;
                response.Success = true;
                response.Message = "Game created successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating game: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<GameDto>> UpdateAsync(int id, GameUpdateDto dto)
        {
            var response = new ServiceResponse<GameDto>();

            var game = await _repository.GetByIdAsync(id);
            if (game == null)
            {
                response.Success = false;
                response.Message = "Game not found";
                return response;
            }


            game.Title = dto.Title;
            game.Genre = dto.Genre;
            game.Description = dto.Description;
            game.Price = dto.Price;

            await _repository.UpdateAsync(game);

         
            string gameFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "games", game.Id.ToString());

            
            if (dto.Images != null && dto.Images.Count > 0)
            {
                
                if (Directory.Exists(gameFolder))
                {
                    Directory.Delete(gameFolder, true);
                }

                Directory.CreateDirectory(gameFolder);

                
                foreach (var image in dto.Images)
                {
                    string filePath = Path.Combine(gameFolder, image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
            }

            response.Success = true;
            response.Message = "Game updated successfully";
            response.Data = new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Genre = game.Genre,
                Description = game.Description,
                Price = game.Price
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<bool>();

            var game = await _repository.GetByIdAsync(id);
            if (game == null)
            {
                response.Success = false;
                response.Message = "Game not found";
                return response;
            }

            await _repository.DeleteAsync(id);

            response.Success = true;
            response.Message = "Game deleted successfully";
            response.Data = true;

            return response;
        }


        public async Task<ServiceResponse<GameDto>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<GameDto>();
            var game = await _repository.GetByIdAsync(id);

            if (game == null)
            {
                response.Success = false;
                response.Message = "Game not found";
                return response;
            }

            response.Success = true;
            response.Data = new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Genre = game.Genre,
                Description = game.Description,
                Price = game.Price
            };

            return response;
        }


        public async Task<ServiceResponse<List<GameDto>>> GetAllAsync()
        {
            var response = new ServiceResponse<List<GameDto>>();
            var games = await _repository.GetAllAsync();

            response.Success = true;
            response.Data = games.Select(g => new GameDto
            {
                Id = g.Id,
                Title = g.Title,
                Genre = g.Genre,
                Description = g.Description,
                Price = g.Price
            }).ToList();

            return response;
        }


        public async Task<ServiceResponse<List<GameDto>>> GetByGenreAsync(string genre)
        {
            var response = new ServiceResponse<List<GameDto>>();
            var games = await _repository.GetByGenreAsync(genre);

            if (games == null || !games.Any())
            {
                response.Success = false;
                response.Message = "No games found for this genre";
                return response;
            }

            response.Success = true;
            response.Data = games.Select(g => new GameDto
            {
                Id = g.Id,
                Title = g.Title,
                Genre = g.Genre,
                Description = g.Description,
                Price = g.Price
            }).ToList();

            return response;
        }

        //   async Task<ServiceResponse<Game>> IGameService.CreateAsync(GameDto dto)
        //    {
        //        var response = new ServiceResponse<GameDto>();

        //        try
        //        {
        //            var game = new Game
        //            {
        //                Title = dto.Title,
        //                Genre = dto.Genre,
        //                Description = dto.Description,
        //                Price = dto.Price
        //            };

        //            await _repository.CreateAsync(game);


        //            string folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "games", game.Id.ToString());
        //            Directory.CreateDirectory(folderPath);

        //            response.Data = dto;
        //            response.Success = true;
        //            response.Message = "Game created successfully!";
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Success = false;
        //            response.Message = $"Error creating game: {ex.Message}";
        //        }

        //        return response;
        //    }

        //    Task<ServiceResponse<Game>> IGameService.UpdateAsync(int id, GameUpdateDto dto)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    Task<ServiceResponse<Game>> IGameService.GetByIdAsync(int id)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    Task<ServiceResponse<IEnumerable<Game>>> IGameService.GetAllAsync()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    Task<ServiceResponse<IEnumerable<Game>>> IGameService.GetByGenreAsync(string genre)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}