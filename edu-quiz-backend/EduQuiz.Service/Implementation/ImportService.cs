using EduQuiz.DomainEntities.Domain;
using EduQuiz.Repository.Interface;
using EduQuiz.Service.Interface;
using ExcelDataReader;
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

        public ImportService(IRepository<Quiz> quizRepository)
        {
            _quizRepository = quizRepository;
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

                    // Create or get existing question
                    if (!questionsDict.ContainsKey(questionId))
                    {
                        questionsDict[questionId] = new Question
                        {
                            QuestionText = questionText,
                            Answers = new List<Answer>()
                        };
                    }

                    // Add answer
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
    }
}
