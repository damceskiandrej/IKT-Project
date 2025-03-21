using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudyBuddy.DomainEntities.Domain;
using StudyBuddy.DomainEntities.DTO.Request;
using System.Text.Json;

namespace StudyBuddyWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _initialUrl = "https://quizapi.io/api/v1/questions?apiKey=v0X51vT9BPXbCuPrgBybFKHVHkomwqr9Az5VaL95";

        public QuizController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(_initialUrl);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<List<QuizRequest>>(jsonResponse);
            return new OkObjectResult(jsonData);
        }
    }
}
