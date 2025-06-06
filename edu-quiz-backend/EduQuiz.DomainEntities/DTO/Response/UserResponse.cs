﻿using EduQuiz.DomainEntities.Roles;
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
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public EduQuizRole Role { get; set; }
    }
}
