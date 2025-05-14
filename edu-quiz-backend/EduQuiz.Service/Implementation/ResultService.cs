using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Request;
using EduQuiz.DomainEntities.DTO.Response;
using EduQuiz.Repository.Interface;
using EduQuiz.Service.Interface;
using Microsoft.IdentityModel.Tokens;

namespace EduQuiz.Service.Implementation;

public class ResultService : IResultService
{

    private readonly IQuizRepository _quizRepository;
    private readonly IResultRepository _resultRepository;
    private readonly IReccomendationRepository _reccomendationRepository;
    private readonly IQuizService _quizService;

    public ResultService(IQuizRepository quizRepository, IResultRepository resultRepository, IReccomendationRepository reccomendationRepository, IQuizService quizService)
    {
        _quizRepository = quizRepository;
        _resultRepository = resultRepository;
        _reccomendationRepository = reccomendationRepository;
        _quizService = quizService;
    }

    public async Task<QuizResultResponse> ProcessResult(QuizResultRequest request)
    {
        var response = new QuizResultResponse();

        await AddToReccomendationTable(request);

        // Validate quiz exists
        var quiz = await _quizRepository.GetById(request.QuizId);
        if (quiz == null)
        {
            response.IsSuccess = false;
            response.Message = $"Quiz {request.QuizId} not found";
            return response;
        }

        // Validate each question
        foreach (var questionResult in request.QuestionResults)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.Id == questionResult.QuestionId);
            if (question == null)
            {
                response.IsSuccess = false;
                response.Message = $"Question {questionResult.QuestionId} not found in quiz";
                return response;
            }

            // Validate each answer
            foreach (var answerId in questionResult.SelectedAnswerIds)
            {
                if (!question.Answers.Any(a => a.Id == answerId))
                {
                    response.IsSuccess = false;
                    response.Message = $"Answer {answerId} not found in question";
                    return response;
                }
            }
        }
        // Calculate score
        int correctAnswers = 0;
        int totalQuestions = quiz.Questions.Count;

        foreach (var questionResult in request.QuestionResults)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.Id == questionResult.QuestionId);
            if (question != null)
            {
                var correctAnswerIds = question.Answers.Where(a => a.isCorrect).Select(a => a.Id).ToList();

                // Check if selected answers match correct answers
                if (question.HasMultipleCorrectAnswers)
                {
                    // For multiple correct answers, all correct answers must be selected and no incorrect ones
                    if (questionResult.SelectedAnswerIds.Count == correctAnswerIds.Count &&
                        questionResult.SelectedAnswerIds.All(correctAnswerIds.Contains))
                    {
                        correctAnswers++;
                    }
                }
                else
                {
                    // For single correct answer, only one answer should be selected and it should be correct
                    if (questionResult.SelectedAnswerIds.Count == 1 &&
                        correctAnswerIds.Contains(questionResult.SelectedAnswerIds.First()))
                    {
                        correctAnswers++;
                    }
                }
            }
        }

        // Calculate score as percentage
        int score = totalQuestions > 0 ? (correctAnswers * 100) / totalQuestions : 0;

        // Save result
        var result = new Result
        {
            UserId = request.UserId,
            QuizId = request.QuizId,
            Score = score,
            NumberOfAttempts = await _resultRepository.GetAttemptCount(request.UserId, request.QuizId) + 1,
            UserAnswers = request.QuestionResults.Select(qr => new UserAnswer
            {
                QuestionId = qr.QuestionId,
                SelectedAnswerIds = qr.SelectedAnswerIds
            }).ToList()
        };

        _resultRepository.Insert(result);

        // Return response
        response.IsSuccess = true;
        response.Message = "Quiz completed successfully";
        response.ResultId = result.Id;
        response.Score = score;
        response.TotalQuestions = totalQuestions;
        response.CorrectAnswers = correctAnswers;

        return response;
    }

    private async Task AddToReccomendationTable(QuizResultRequest request)
    {
        try
        {
            var reccomendationForQuiz = await _reccomendationRepository.GetReccomendationByQuizId(request.QuizId);
            var hasReccomendationForQuiz = HasReccomendation(reccomendationForQuiz);

            if (hasReccomendationForQuiz)
            {
                return;
            }

            var quizExplainations = await _quizService.GetQuizSummaryAsync(request.QuizId);


            foreach (var explaination in quizExplainations)
            {

                var reccomendation = new Reccomendation
                {
                    UserId = request.UserId,
                    QuizId = request.QuizId,
                    Question = explaination.Question,
                    Explanation = explaination.Explanation
                };
                _reccomendationRepository.InsertReccomendation(reccomendation);
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    private bool HasReccomendation(List<Reccomendation> reccomendationsForQuiz)
    {
        return !reccomendationsForQuiz.IsNullOrEmpty();
    }
}