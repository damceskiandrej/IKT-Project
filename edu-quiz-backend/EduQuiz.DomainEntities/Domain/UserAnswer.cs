namespace EduQuiz.DomainEntities.Domain;

public class UserAnswer : BaseEntity
{
    public Guid ResultId { get; set; }
    public Guid QuestionId { get; set; }
    public List<Guid> SelectedAnswerIds { get; set; }
    public Result Result { get; set; }
}