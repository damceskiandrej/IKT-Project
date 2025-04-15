using EduQuiz.DomainEntities.DTO.Request;
using EduQuiz.DomainEntities.DTO.Response;

namespace EduQuiz.Service.Interface;

public interface IResultService
{
 
        Task<QuizResultResponse> ProcessResult(QuizResultRequest request);
    
}