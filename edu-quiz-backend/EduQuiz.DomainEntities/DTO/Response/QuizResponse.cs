namespace EduQuiz.DomainEntities.DTO.Response;

public class QuizResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public int QuestionCount { get; set; }
}