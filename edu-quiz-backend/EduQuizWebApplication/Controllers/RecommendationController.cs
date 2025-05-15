using EduQuiz.DomainEntities.DTO.Response;
using EduQuiz.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduQuizWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IReccomendationService _reccomendationService;

        public RecommendationController(IReccomendationService reccomendationService)
        {
            _reccomendationService = reccomendationService;
        }
        [HttpGet("{quizId}/{userId}")]
        public async Task<List<QuizExplanationResponse>> GetQuizExplanationResponses(Guid quizId, string userId)
        {
            var result = await _reccomendationService.GetReccomendationQuizIdAndUserId(quizId, userId);
            return result;
        }
    }
}
