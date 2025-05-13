namespace EduQuiz.DomainEntities.DTO.Response;

public class ResultResponse
{
    public Guid ResultId { get; set; }
    public int Score { get; set; }
    public int NumberOfAttempts { get; set; }
    public List<UserAnswerResponse> UserAnswers { get; set; }
}