namespace EduQuiz.DomainEntities.DTO.Response;

public class QuestionSummaryDto
{
    public string QuestionText { get; set; }
    public List<AnswerSummaryDto> Answers { get; set; }
}