using EduQuiz.DomainEntities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.Domain
{
    public class Quiz : BaseEntity
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<EduQuizUser> Users { get; set; }
    }
}
