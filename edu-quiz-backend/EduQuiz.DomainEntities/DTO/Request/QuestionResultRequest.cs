namespace EduQuiz.DomainEntities.DTO.Request
{
    
public class QuestionResultRequest
{
    public Guid QuestionId { get; set; }
    public List<Guid> SelectedAnswerIds { get; set; }
}
}
