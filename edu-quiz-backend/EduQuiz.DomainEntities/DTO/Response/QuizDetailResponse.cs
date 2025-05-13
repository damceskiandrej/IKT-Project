namespace EduQuiz.DomainEntities.DTO.Response;

public class QuizDetailResponse : QuizResponse
{
    public new List<QuestionDetailResponse> Questions { get; set; }
}