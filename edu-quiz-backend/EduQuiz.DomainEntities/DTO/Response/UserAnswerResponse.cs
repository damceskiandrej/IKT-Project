namespace EduQuiz.DomainEntities.DTO.Response;

public class UserAnswerResponse
{
    public Guid QuestionId { get; set; }
    public List<Guid> SelectedAnswerIds { get; set; }
}