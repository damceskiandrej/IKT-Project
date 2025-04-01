using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.Domain
{
    public class Question : BaseEntity
    {
        public Quiz Quiz { get; set; }
        public Guid QuizId { get; set; }
        public string QuestionText { get; set; }
        public bool HasMultipleCorrectAnswers {  get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
