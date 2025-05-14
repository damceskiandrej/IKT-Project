using EduQuiz.DomainEntities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.Domain
{
    public class Reccomendation : BaseEntity
    {
        public string UserId { get; set; }
        public Guid QuizId { get; set; }
        public string Question { get; set; }
        public string Explanation {  get; set; }
    }
}
