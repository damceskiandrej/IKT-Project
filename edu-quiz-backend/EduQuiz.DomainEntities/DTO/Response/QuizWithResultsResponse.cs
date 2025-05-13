namespace EduQuiz.DomainEntities.DTO.Response;

public class QuizWithResultsResponse
{
    public QuizDetailResponse Quiz { get; set; }
    public List<ResultResponse> Results { get; set; }
}