using EduQuiz.DomainEntities.DTO.Response;

namespace EduQuiz.Service.Interface;

public interface IAIService
{
    Task<string> GetHintAsync(string question, List<string> answers);
    Task<List<QuizExplanationResponse>> GetQuizSummaryAsync(List<QuestionSummaryDto> questions);
}