using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.DTO.Response
{
    public class AnswerResponse
    {
        public Guid Id { get; set; }
        public string? AnswerText { get; set; } // The text of the answer
        public bool IsCorrect { get; set; } // Whether the answer is correct or not
    }
}
