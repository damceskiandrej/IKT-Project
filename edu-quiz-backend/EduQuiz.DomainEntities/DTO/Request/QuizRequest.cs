using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.DTO.Request
{
    public class QuizRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("answers")]
        public AnswersRequest Answers { get; set; }

        [JsonPropertyName("multiple_correct_answers")]
        public bool MultipleCorrectAnswers { get; set; }

        [JsonPropertyName("correct_answers")]
        public CorrectAnswersRequest Correct_Answers { get; set; }

        [JsonPropertyName("correct_answer")]
        public string CorrectAnswer { get; set; }

        [JsonPropertyName("explanation")]
        public string Explanation { get; set; }

        [JsonPropertyName("tip")]
        public string Tip { get; set; }

        [JsonPropertyName("tags")]
        public List<TagRequest> Tags { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; }
    }
}
