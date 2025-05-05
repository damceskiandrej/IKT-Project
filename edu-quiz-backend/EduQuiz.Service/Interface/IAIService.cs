using EduQuiz.DomainEntities.DTO.Response;

namespace EduQuiz.Service.Interface;

public interface IAIService
{
    Task<string> GetHintAsync(string question, List<string> answers);
    Task<string> GetQuizSummaryAsync(List<QuestionSummaryDto> questions);
}