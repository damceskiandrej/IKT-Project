namespace EduQuiz.DomainEntities.DTO.Response;

public class QuestionDetailResponse : QuestionResponse
{
    public Guid QuestionId { get; set; }
    public new List<AnswerDetailResponse> Answers { get; set; }
}