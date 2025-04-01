using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Request;
using System.Text.Json;
using EduQuiz.Service.Interface;

namespace EduQuizWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _initialUrl = "https://quizapi.io/api/v1/questions?apiKey=v0X51vT9BPXbCuPrgBybFKHVHkomwqr9Az5VaL95";
        private readonly IQuizService _quizService;

        public QuizController(IHttpClientFactory httpClientFactory, IQuizService quizService)
        {
            _httpClientFactory = httpClientFactory;
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
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
    }
}
