using Business.Abstracts;
using Business.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel request)
        {
            var result = await _userService.CreateUser(request);

            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel request)
        {
            var loginResult = await _userService.Login(request);

            return Ok(loginResult);
        }
    }
}
