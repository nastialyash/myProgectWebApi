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
                var response = await _service.GetAllAsync();
                return Ok(response);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var response = await _service.GetByIdAsync(id);
                if (response == null) return NotFound();
                return Ok(response);
            }

            [HttpGet("genre/{genre}")]
            public async Task<IActionResult> GetByGenre(string genre)
            {
                var response = await _service.GetByGenreAsync(genre);
                return Ok(response);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] GameDto dto)
            {
                var game = new Game { Genre = dto.Genre, Name = dto.Name, ReleaseDate=dto.ReleaseDate,Price = dto.Price,Id=dto.Id  };
                
           var response = _service.CreateAsync(game);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, GameUpdateDto dto)
            {
                if (id != dto.Id) return BadRequest();
                var game = new Game { Genre = dto.Genre, Name = dto.Name, ReleaseDate = dto.ReleaseDate, Price = dto.Price, Id = dto.Id };

                var response = await _service.UpdateAsync(game);
                if (response == null) return NotFound();
                return Ok(response);

            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var response = await _service.DeleteAsync(id);
                if (!response) return NotFound();
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
