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
        public async Task<IActionResult> Register([FromBody] UserRequest request)
        {
            var result = await _userService.RegisterUser(request);
            return Ok(result);
        }
    }
}
