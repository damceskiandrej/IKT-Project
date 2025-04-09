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

namespace EduQuiz.Service.Implementation
{
    public class QuizService : IQuizService
    {

        public readonly IQuizRepoistory _quizRepoistory;
        public readonly UserManager<EduQuizUser> _userManager;

        public QuizService(IQuizRepoistory quizRepoistory, UserManager<EduQuizUser> userManager)
        {
            _quizRepoistory = quizRepoistory;
            _userManager = userManager;
        }

        public Task PopulateData(List<QuizRequest> quizRequests)
        {
            //TODO:
            //If we want each quiz to be from the same category,
            //we might need additional loop, as of now, each http request
            //represents one quiz

            var quiz = new Quiz();
            quiz.Title = "Random Quiz";
            quiz.Category = "Category 1";

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

            _quizRepoistory.Insert(quiz);

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
                var quiz = await _quizRepoistory.GetQuizByCategory(category);
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
                    _quizRepoistory.Insert(quiz);
                }
                else
                {
                    _quizRepoistory.Update(quiz);
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
        //    var existingQuizzes = await _quizRepoistory.GetQuizzesByCategories(allUniqueCategories);
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
        //        await _quizRepoistory.InsertMany(quizzesToInsert);
        //    }

        //    if (quizzesToUpdate.Any())
        //    {
        //        await _quizRepoistory.UpdateMany(quizzesToUpdate);
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

        public async Task<ResponseModel> InsertQuizzesForUser(string userId, List<string> quizIds)
        {
            var result = new ResponseModel();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var quizzes = await _quizRepoistory.GetQuizzesByIds(quizIds);

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
    }
}
