using Azure.Core;
using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Response;
using EduQuiz.DomainEntities.Identity;
using EduQuiz.Repository.Interface;
using EduQuiz.Service.Interface;
using ExcelDataReader;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Implementation
{
    public class ImportService : IImportService
    {
        private readonly IRepository<Quiz> _quizRepository;
        private readonly UserManager<EduQuizUser> _userManager;

        public ImportService(IRepository<Quiz> quizRepository, UserManager<EduQuizUser> userManager)
        {
            _quizRepository = quizRepository;
            _userManager = userManager;
        }

        public async Task<Quiz> GetQuizFromFile(string fileName)
        {
            var quiz = new Quiz();
            var questionsDict = new Dictionary<string, Question>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                while (reader.Read())
                {
                    if (quiz.Title.IsNullOrEmpty() && quiz.Category.IsNullOrEmpty()) {
                        quiz.Title = reader.GetValue(0)?.ToString();
                        quiz.Category = reader.GetValue(1)?.ToString();
                    }
                    string questionId = reader.GetValue(2)?.ToString();
                    string questionText = reader.GetValue(3)?.ToString();
                    string answerText = reader.GetValue(4)?.ToString();
                    bool isCorrect = bool.TryParse(reader.GetValue(5)?.ToString(), out var parsed) && parsed;

                    if (string.IsNullOrWhiteSpace(questionId))
                        continue;

                    if (!questionsDict.ContainsKey(questionId))
                    {
                        questionsDict[questionId] = new Question
                        {
                            QuestionText = questionText,
                            Answers = new List<Answer>()
                        };
                    }

                    questionsDict[questionId].Answers.Add(new Answer
                    {
                        AnswerText = answerText,
                        isCorrect = isCorrect
                    });
                }
            }

            quiz.Questions = questionsDict.Values.ToList();
            _quizRepository.Insert(quiz);
            return quiz;

        }

        public async Task<List<UserResponse>> GetStudentsFromFile(string fileName)
        {
            var response = new List<UserResponse>();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "files", fileName);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                while (reader.Read())
                {
                    var result = new UserResponse();

                    try
                    {
                        var createdStudent = getStudent(reader);
                        var password = reader.GetString(4);

                        if (await _userManager.FindByEmailAsync(createdStudent.Email) != null)
                        {
                            result.Message = "Email Already Exists";
                            result.IsSuccess = false;
                            response.Add(result);
                            continue;
                        }

                        if (await _userManager.FindByNameAsync(createdStudent.UserName) != null)
                        {
                            result.Message = "Username Already Exists";
                            result.IsSuccess = false;
                            response.Add(result);
                            continue;
                        }

                        var userCreated = await _userManager.CreateAsync(createdStudent, password);
                        if (!userCreated.Succeeded)
                        {
                            result.Message = "User Creation Failed";
                            result.IsSuccess = false;
                            response.Add(result);
                            continue;
                        }

                        result.Message = "User Created";
                        result.UserName = createdStudent.UserName;
                        result.UserId = createdStudent.Id;
                        result.FirstName = createdStudent.FirstName;
                        result.LastName = createdStudent.LastName;
                        result.Email = createdStudent.Email;
                        result.IsSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        result.Message = $"Error: {ex.Message}";
                        result.IsSuccess = false;
                    }

                    response.Add(result);
                }
            }

            //TODO: response.Where(i => i.IsSuccess).Count() can be implemented on the frontend
            var successfullyAddedStudentsCount = response.Where(i => i.IsSuccess).Count();
            response.Add(new UserResponse
            {
                Message = $"Sucessfully added {successfullyAddedStudentsCount} new students"
            });
            return response;
        }


        private EduQuizUser getStudent(IExcelDataReader reader)
        {
            return new EduQuizUser
            {
                FirstName = reader.GetString(0),
                LastName = reader.GetString(1),
                Email = reader.GetString(2),
                UserName = reader.GetString(3),
            };
        }
    }
}
