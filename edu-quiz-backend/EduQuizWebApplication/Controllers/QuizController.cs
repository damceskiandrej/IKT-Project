using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Request;
using System.Text.Json;
using EduQuiz.Service.Interface;

namespace EduQuizWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _initialUrl = "https://quizapi.io/api/v1/questions?apiKey=v0X51vT9BPXbCuPrgBybFKHVHkomwqr9Az5VaL95";
        private readonly IQuizService _quizService;
        private readonly IResultService _resultService;

        public QuizController(IHttpClientFactory httpClientFactory, IQuizService quizService, IResultService resultService)
        {
            _httpClientFactory = httpClientFactory;
            _quizService = quizService;
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<IActionResult> FetchExternal()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(_initialUrl);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<List<QuizRequest>>(jsonResponse);
            if (jsonData != null)
            {
                await _quizService.PopulateDataPerCategory(jsonData);
            }
            return new OkObjectResult(jsonData);
        }


        [HttpGet]
        public async Task<IActionResult> PopulateDatabase()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(_initialUrl);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<List<QuizRequest>>(jsonResponse);
            if (jsonData != null)
            {
                await _quizService.PopulateDataPerCategory(jsonData);
            }
            return Ok(new { message = "Database populated successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzes();
            return Ok(quizzes);
        }

        [HttpGet]
        public async Task<IActionResult> GetQuiz(Guid id)
        {
            var quiz = await _quizService.GetQuizById(id);
            if (quiz == null)
            {
                return NotFound(new { message = "Quiz not found" });
            }
            return Ok(quiz);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetQuizzesByCategory(string category)
        {
            var quizzes = await _quizService.GetQuizzesByCategory(category);
            return Ok(quizzes);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetQuizzesByUser(string userId)
        {
            var quizzes = await _quizService.GetQuizzesByUser(userId);
            return Ok(quizzes);
        }
        
        [HttpPost]
        public async Task<IActionResult> SubmitQuizResult([FromBody] QuizResultRequest request)
        {
            var result = await _resultService.ProcessResult(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{userId}/{quizId}")]
        public async Task<IActionResult> GetQuizByUser(string userId, Guid quizId)
        {
            var response = await _quizService.GetQuizByUser(userId, quizId);
            return response == null ? NotFound() : Ok(response);
        }
    }
}
