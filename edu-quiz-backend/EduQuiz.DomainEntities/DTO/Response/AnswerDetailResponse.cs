namespace EduQuiz.DomainEntities.DTO.Response;

public class AnswerDetailResponse : AnswerResponse
{
    public Guid AnswerId { get; set; }
    public bool IsCorrect { get; set; } 
}