using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Request;
using EduQuiz.DomainEntities.DTO.Response;
using EduQuiz.DomainEntities.Identity;
using EduQuiz.Repository.Interface;
using EduQuiz.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduQuiz.DomainEntities.DTO.Response;

namespace EduQuiz.Service.Implementation
{
    public class QuizService : IQuizService
    {

        public IQuizRepository _quizRepository;
        private readonly IResultRepository _resultRepository;
        private readonly UserManager<EduQuizUser> _userManager;
        private readonly IAIService _aiService;

        public QuizService(IQuizRepository quizRepository, IResultRepository resultRepository, UserManager<EduQuizUser> userManager, IAIService aiService)
        {   
            _quizRepository = quizRepository;
            _resultRepository = resultRepository;
            _userManager = userManager;
            _aiService = aiService;
        }

        public Task PopulateData(List<QuizRequest> quizRequests)
        {
            //TODO:
            //If we want each quiz to be from the same category,
            //we might need additional loop, as of now, each http request
            //represents one quiz

            var quiz = new Quiz();
            quiz.Title = "Quiz of the day";
            quiz.Category = "Misterious category";

            var questions = new List<Question>();

            foreach (var quizRequest in quizRequests)
            {
                var indexOfCorrectAnswer = GetCorrectAnswerIndex(quizRequest.Correct_Answers);
                var allAnswers = GetAllAnswersFromRequest(quizRequest.Answers);

                var question = new Question()
                {
                    QuestionText = quizRequest.Question,
                    HasMultipleCorrectAnswers = quizRequest.MultipleCorrectAnswers,
                    Answers = GetAnswer(allAnswers, indexOfCorrectAnswer),
                };


                questions.Add(question);
                quiz.Questions = questions;

            }

            _quizRepository.Insert(quiz);

            return Task.CompletedTask;
        }

        //Might need some refactoring
        public async Task PopulateDataPerCategory(List<QuizRequest> quizRequests)
        {

            var allTags = quizRequests.SelectMany(x => x.Tags.Select(y => y.Name)).ToList();
            var allCategories = quizRequests.Select(x => x.Category).ToList();

            var allUniqueCategories = allTags.Concat(allCategories).Distinct();

            foreach (var category in allUniqueCategories)
            {
                var quiz = await _quizRepository.GetQuizByCategory(category);
                var insertOperation = false;
                if (quiz == null)
                {
                    insertOperation = true;
                    quiz = new Quiz();
                    quiz.Title = $"Quiz for {category}";
                    quiz.Category = category;
                }
                var questions = new List<Question>();
                var quizForCategory = quizRequests.Where(q => q.Category.Equals(category) || q.Tags.Select(x => x.Name).Contains(category)).ToList();

                foreach (var quizRequest in quizForCategory)
                {
                    var indexOfCorrectAnswer = GetCorrectAnswerIndex(quizRequest.Correct_Answers);
                    var allAnswers = GetAllAnswersFromRequest(quizRequest.Answers);

                    var question = new Question()
                    {
                        QuestionText = quizRequest.Question,
                        HasMultipleCorrectAnswers = quizRequest.MultipleCorrectAnswers,
                        Answers = GetAnswer(allAnswers, indexOfCorrectAnswer),
                    };

                    questions.Add(question);
                    quiz.Questions = questions;

                }

                if (insertOperation)
                {
                    _quizRepository.Insert(quiz);
                }
                else
                {
                    _quizRepository.Update(quiz);
                }

            }
        }

        //better version, some methods need to be implemented
        //public async Task PopulateDataPerCategory(List<QuizRequest> quizRequests)
        //{
        //    // Get all unique categories
        //    var allTags = quizRequests.SelectMany(x => x.Tags.Select(y => y.Name)).ToList();
        //    var allCategories = quizRequests.Select(x => x.Category).ToList();
        //    var allUniqueCategories = allTags.Concat(allCategories).Distinct().ToList();

        //    // Fetch all quizzes for these categories in a single batch operation if possible
        //    var existingQuizzes = await _quizRepository.GetQuizzesByCategories(allUniqueCategories);
        //    var quizzesByCategory = existingQuizzes.ToDictionary(q => q.Category, q => q);

        //    var quizzesToUpdate = new List<Quiz>();
        //    var quizzesToInsert = new List<Quiz>();

        //    foreach (var category in allUniqueCategories)
        //    {
        //        // Check if we already have this quiz
        //        if (!quizzesByCategory.TryGetValue(category, out var quiz))
        //        {
        //            // Create new quiz if it doesn't exist
        //            quiz = new Quiz
        //            {
        //                Title = $"Quiz for {category}",
        //                Category = category,
        //                Questions = new List<Question>()
        //            };
        //            quizzesToInsert.Add(quiz);
        //        }
        //        else
        //        {
        //            quizzesToUpdate.Add(quiz);
        //        }

        //        // Find all quiz requests relevant to this category
        //        var quizForCategory = quizRequests.Where(q =>
        //            q.Category.Equals(category) ||
        //            q.Tags.Select(x => x.Name).Contains(category)).ToList();

        //        // Create questions list (might want to preserve existing questions here)
        //        var questions = new List<Question>();

        //        foreach (var quizRequest in quizForCategory)
        //        {
        //            var indexOfCorrectAnswer = GetCorrectAnswerIndex(quizRequest.Correct_Answers);
        //            var allAnswers = GetAllAnswersFromRequest(quizRequest.Answers);

        //            var question = new Question
        //            {
        //                QuestionText = quizRequest.Question,
        //                HasMultipleCorrectAnswers = quizRequest.MultipleCorrectAnswers,
        //                Answers = GetAnswer(allAnswers, indexOfCorrectAnswer)
        //            };

        //            questions.Add(question);
        //        }

        //        quiz.Questions = questions;
        //    }

        //    // Batch operations instead of individual updates
        //    if (quizzesToInsert.Any())
        //    {
        //        await _quizRepository.InsertMany(quizzesToInsert);
        //    }

        //    if (quizzesToUpdate.Any())
        //    {
        //        await _quizRepository.UpdateMany(quizzesToUpdate);
        //    }
        //}
        private List<Answer> GetAnswer(List<string> allAnswers, int correctIndex)
        {
            return allAnswers.Select((answerText, index) => new Answer
            {
                AnswerText = answerText != null ? answerText : "n/a",
                isCorrect = index == correctIndex
            }).ToList();
        }

        private List<string> GetAllAnswersFromRequest(AnswersRequest answers)
        {
            return new List<string>
            {
                answers.Answer_A,
                answers.Answer_B,
                answers.Answer_C,
                answers.Answer_D,
                answers.Answer_E,
                answers.Answer_F,
            };
        }

        private int GetCorrectAnswerIndex(CorrectAnswersRequest correctAnswer)
        {
            return correctAnswer switch
            {
                { Answer_A_Correct: true } => 0,
                { Answer_B_Correct: true } => 1,
                { Answer_C_Correct: true } => 2,
                { Answer_D_Correct: true } => 3,
                { Answer_E_Correct: true } => 4,
                { Answer_F_Correct: true } => 5,
                _ => -1
            };
        }
        
        
        public async Task<List<QuizResponse>> GetAllQuizzes()
        {
            return await _quizRepository.GetAllMapped();
        }

        public async Task<QuizResponse> GetQuizById(Guid id)
        {
            var quiz = await _quizRepository.GetById(id);
            if (quiz == null)
            {
                return null;
            }

            return new QuizResponse
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Category = quiz.Category,
                QuestionCount = quiz.Questions?.Count ?? 0,
                Questions = quiz.Questions?.Select(q => new QuestionResponse
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    Answers = q.Answers?.Select(a => new AnswerResponse
                    {
                        Id = a.Id,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.isCorrect
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<ResponseModel> InsertQuizzesForUser(string userId, List<string> quizIds)
        {
            var result = new ResponseModel();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var quizzes = await _quizRepository.GetQuizzesByIds(quizIds.Select(Guid.Parse).ToList());


                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Message = $"Quizzes for User with Id: {userId} Not added";
                    return result;
                }

                var userQuizzes = user.Quizzes;
                if (userQuizzes.Any())
                {
                    //userQuizzes.ToList().AddRange(quizzes);
                    foreach (var quiz in quizzes)
                    {
                        userQuizzes.Add(quiz);
                    }
                }

                user.Quizzes = userQuizzes;
                await _userManager.UpdateAsync(user);
                result.IsSuccess = true;
                result.Message = $"Quizzes for User with Id: {userId} Added Successfully";
                return result;
               
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
          

        }

        public async Task<List<QuizResponse>> GetQuizzesByCategory(string category)
        {
            var quizzes = await _quizRepository.GetQuizzesByCategory(category);
            return quizzes.Select(q => new QuizResponse
            {
                Id = q.Id,
                Title = q.Title,
                Category = q.Category,
                QuestionCount = q.Questions?.Count ?? 0
            }).ToList();
        }

        public async Task<List<QuizResponse>> GetQuizzesByUser(string userId)
        {
            var results = await _resultRepository.GetResultsByUser(userId);
            var quizIds = results.Select(r => r.QuizId).Distinct().ToList();
            var quizzes = await _quizRepository.GetQuizzesByIds(quizIds);

            return quizzes.Select(q => new QuizResponse
            {
                Id = q.Id,
                Title = q.Title,
                Category = q.Category,
                QuestionCount = q.Questions?.Count ?? 0
            }).ToList();
        }
        
        public async Task<string> GetQuestionHintAsync(Guid quizId, Guid questionId)
        {
            var quiz = await _quizRepository.GetById(quizId);
            var question = quiz?.Questions.FirstOrDefault(q => q.Id == questionId);
        
            if (question == null) throw new KeyNotFoundException("Question not found");
        
            return await _aiService.GetHintAsync(
                question.QuestionText,
                question.Answers.Select(a => a.AnswerText).ToList()
            );
        }

        public async Task<List<QuizExplanationResponse>> GetQuizSummaryAsync(Guid quizId)
        {
            var quiz = await _quizRepository.GetById(quizId);
            if (quiz == null) throw new KeyNotFoundException("Quiz not found");

            var questions = quiz.Questions.Select(q => new QuestionSummaryDto
            {
                QuestionText = q.QuestionText,
                Answers = q.Answers.Select(a => new AnswerSummaryDto
                {
                    AnswerText = a.AnswerText,
                    IsCorrect = a.isCorrect
                }).ToList()
            }).ToList();

            return await _aiService.GetQuizSummaryAsync(questions);
        }
        
        public async Task<QuizWithResultsResponse> GetQuizByUser(string userId, Guid quizId)
        {
            var quiz = await _quizRepository.GetById(quizId);
            if (quiz == null) return null;

            var results = await _resultRepository.GetResultsByUserAndQuiz(userId, quizId);
            if (!results.Any()) return null;

            return new QuizWithResultsResponse
            {
                Quiz = MapToQuizDetail(quiz),
                Results = results.Select(r => MapToResultResponse(r)).ToList()
            };
        }

        private QuizDetailResponse MapToQuizDetail(Quiz quiz)
        {
            return new QuizDetailResponse
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Category = quiz.Category,
                Questions = quiz.Questions.Select(q => new QuestionDetailResponse
                {
                    QuestionId = q.Id,
                    QuestionText = q.QuestionText,
                    HasMultipleCorrectAnswers = q.HasMultipleCorrectAnswers,
                    Answers = q.Answers.Select(a => new AnswerDetailResponse
                    {
                        AnswerId = a.Id,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.isCorrect
                    }).ToList()
                }).ToList()
            };
        }

        private ResultResponse MapToResultResponse(Result result)
        {
            return new ResultResponse
            {
                ResultId = result.Id,
                Score = result.Score,
                NumberOfAttempts = result.NumberOfAttempts,
                UserAnswers = result.UserAnswers.Select(ua => new UserAnswerResponse
                {
                    QuestionId = ua.QuestionId,
                    SelectedAnswerIds = ua.SelectedAnswerIds
                }).ToList()
            };
        }

    }
}
