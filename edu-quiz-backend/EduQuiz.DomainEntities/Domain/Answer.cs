using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.Domain
{
    public class Answer : BaseEntity
    {
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool isCorrect {  get; set; }

    }
}
