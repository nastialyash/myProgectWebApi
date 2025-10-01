using Microsoft.AspNetCore.Mvc;
using myProgectWebApi.DAL.DTOs;
using myProgectWebApi.Services;
namespace myProgectWebApi.Controllers
{ [ApiController]
        [Route("api/[controller]")]
    public class AuthController : Controller
    {

       
          private readonly IUserService _userService;

            public AuthController(IUserService userService)
            {
                _userService = userService;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
            {
                var result = await _userService.RegisterAsync(dto);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            }
        }
    }
   

