using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduQuiz.DomainEntities.DTO.Request;
using EduQuiz.Service.Interface;

namespace EduQuizWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            var result = await _userService.RegisterUser(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var result = await _userService.AuthenticateUser(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var result = await _userService.GetUserProfile(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        // Edit user profile
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProfile(string id, [FromBody] UserUpdateRequest request)
        {
            var result = await _userService.UpdateUserProfile(id, request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
    }
}
