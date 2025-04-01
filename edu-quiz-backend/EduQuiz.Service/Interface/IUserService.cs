using EduQuiz.DomainEntities.DTO.Request;
using EduQuiz.DomainEntities.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Interface
{
    public interface IUserService
    {
        Task<UserResponse> RegisterUser(UserRegisterRequest request);
        Task<UserResponse> AuthenticateUser(UserLoginRequest request);
    }
}
