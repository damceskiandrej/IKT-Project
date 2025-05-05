using EduQuiz.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EduQuizWebApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly IQuizService _quizService;

    public AIController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet("hint/{quizId}/{questionId}")]
    public async Task<IActionResult> GetHint(Guid quizId, Guid questionId)
    {
        try
        {
            var hint = await _quizService.GetQuestionHintAsync(quizId, questionId);
            return Ok(new { Hint = hint });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("summary/{quizId}")]
    public async Task<IActionResult> GetSummary(Guid quizId)
    {
        try
        {
            var summary = await _quizService.GetQuizSummaryAsync(quizId);
            return Ok(new { Summary = summary });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}