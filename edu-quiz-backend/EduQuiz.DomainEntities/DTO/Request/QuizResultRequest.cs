namespace EduQuiz.DomainEntities.DTO.Request;

public class QuizResultRequest
{
    public string UserId { get; set; }
    public Guid QuizId { get; set; }
    public List<QuestionResultRequest> QuestionResults { get; set; }
}