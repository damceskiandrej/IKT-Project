using EduQuiz.DomainEntities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.Domain
{
    public class Result : BaseEntity
    {
        public string UserId { get; set; }
        public Guid QuizId { get; set; }
        public int NumberOfAttempts {  get; set; } 
        public int Score { get; set; }
    }
}
