using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.DTO.Response
{
    public class ResponseModel
    {
        //TODO: We can have more fields here such as status code, isValid, status (failed, created, pending) etc...
        public string Message {  get; set; }
        public bool IsSuccess { get; set; }
    }
}
