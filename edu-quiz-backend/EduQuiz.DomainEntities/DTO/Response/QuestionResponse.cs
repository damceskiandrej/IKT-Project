using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.DTO.Response
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public string? QuestionText { get; set; } // The question text
        public List<AnswerResponse>? Answers { get; set; } // List of answers for the question
       
    }
}
