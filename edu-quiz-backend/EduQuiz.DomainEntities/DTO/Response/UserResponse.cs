using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.DTO.Response
{
    public class UserResponse : ResponseModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
