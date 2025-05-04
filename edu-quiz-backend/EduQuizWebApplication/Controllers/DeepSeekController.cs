using EduQuiz.DomainEntities.DTO.Request;
using EduQuiz.DomainEntities.DTO.Response;
using EduQuiz.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduQuizWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeepSeekController : ControllerBase
    {
        private readonly IDeepSeekService _deepSeekService;

        public DeepSeekController(IDeepSeekService deepSeekService)
        {
            _deepSeekService = deepSeekService;
        }

        [HttpPost]
        public async Task<IActionResult> RequestPrompt([FromBody] PromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest("Prompt is required.");

            var response = await _deepSeekService.GetResponseAsync(request.Prompt);

            return Ok(new PromptResponse { Message = response });
        }
    }
}
