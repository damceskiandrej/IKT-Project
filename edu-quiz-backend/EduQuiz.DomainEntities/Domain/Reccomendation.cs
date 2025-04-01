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
        //TODO: Instead of text we can make it a JSON format
        public string Text {  get; set; }
    }
}
