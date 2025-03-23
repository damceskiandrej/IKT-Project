using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.Domain
{
    public class  CorrectAnswer : BaseEntity
    {
        public bool IsCorrect { get; set; }

        // Foreign Key
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
    }
}
