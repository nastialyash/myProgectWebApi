using Microsoft.AspNetCore.Mvc;
using myProgectWebApi.DAL.DTOs;
using myProgectWebApi.DAL.Entities;
using myProgectWebApi.DAL.Repositories;
using myProgectWebApi.Services;
 

namespace myProjectWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _service;

        public GameController(IGameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result.Message);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result.Message);
            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] GameUpdateDto dto)
        {
            var response = await _service.UpdateAsync(id, dto);
            if (!response.Success)
                return NotFound(response.Message);

            return Ok(response.Data);
        }
    }
}
