using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.DTO.Request
{
    public class UserQuizzesRequest
    {
        public string UserId { get; set; }
        public List<string> QuizzesId { get; set; }
    }
}
