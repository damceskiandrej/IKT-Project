namespace EduQuiz.DomainEntities.DTO.Response;

public class QuizResultResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public Guid ResultId { get; set; }
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
}