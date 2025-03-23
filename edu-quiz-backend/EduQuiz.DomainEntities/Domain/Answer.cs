using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.Domain
{
    public class Answer : BaseEntity
    {
        public string AnswerText { get; set; }

        // Foreign Key
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
