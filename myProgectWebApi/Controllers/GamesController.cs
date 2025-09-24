using Microsoft.AspNetCore.Mvc;
using myProgectWebApi.DAL.DTOs;
using myProgectWebApi.DAL.Entities;
using myProgectWebApi.DAL.Repositories;


namespace myProgectWebApi.Controllers
{
    


    namespace GameApi.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class GamesController : ControllerBase
        {
            private readonly IGameRepository _service;

            public GamesController(IGameRepository repository)
            {
                _service = repository;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var games = await _service.GetAllAsync();
                return Ok(games);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var game = await _service.GetByIdAsync(id);
                if (game == null) return NotFound();
                return Ok(game);
            }

            [HttpGet("genre/{genre}")]
            public async Task<IActionResult> GetByGenre(string genre)
            {
                var games = await _service.GetByGenreAsync(genre);
                return Ok(games);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] GameDto dto)
            {
                var game = new Game { Genre = dto.Genre, Name = dto.Name, ReleaseDate=dto.ReleaseDate,Price = dto.Price,Id=dto.Id  };
                
           var created= _service.CreateAsync(game);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, GameUpdateDto dto)
            {
                if (id != dto.Id) return BadRequest();
                var game = new Game { Genre = dto.Genre, Name = dto.Name, ReleaseDate = dto.ReleaseDate, Price = dto.Price, Id = dto.Id };

                var created = await _service.UpdateAsync(game);
                if (created == null) return NotFound();
                return Ok(created);

            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            [HttpGet("category/{category}")]
            public async Task<IActionResult> GetByCategory(string category)
            {
                var games = await _service.GetByGenreAsync(category);
                return Ok(games);
            }
        }
    }
}
