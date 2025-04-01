using Microsoft.AspNetCore.Identity;
using EduQuiz.DomainEntities.DTO.Request;
using EduQuiz.DomainEntities.Identity;
using EduQuiz.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduQuiz.DomainEntities.DTO.Response;

namespace EduQuiz.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<EduQuizUser> _userManager;

        public UserService(UserManager<EduQuizUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResponse> AuthenticateUser(UserLoginRequest request)
        {
            var result = new UserResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                result.Message = "Email Not Found";
                result.IsSuccess = false;
                return result;
            }

            var passwordMatches =  await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordMatches)
            {
                result.Message = "Passwords do not match";
                result.IsSuccess = false;
                return result;
            }
            result.Message = "User exists";
            result.UserName = user.UserName;
            result.UserId = user.Id;
            result.IsSuccess = true;
            return result;

        }

        public async Task<UserResponse> RegisterUser(UserRegisterRequest request)
        {
            var result = new UserResponse();
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                result.Message = "Email Already Exists";
                result.IsSuccess = false;
                return result;
            }        
            if (await _userManager.FindByNameAsync(request.Username) != null)
            {
                result.Message = "Username Already Exists";
                result.IsSuccess = false;
                return result;
            }

            try
            {
                var user = new EduQuizUser()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.Username,
                };
                var userCreated = await  _userManager.CreateAsync(user, request.Password);
                if(!userCreated.Succeeded)
                {
                    result.Message = "User Creation Failed";
                    result.IsSuccess = false;
                    return result;
                }
                result.Message = "User Created";
                result.UserName = user.UserName;
                result.UserId = user.Id;
                result.IsSuccess = true;
                return result;

            }
            catch(Exception e)
            {
                result.Message = $"Exception - {e.Message}";
                result.IsSuccess = false;
                return result;
            }
               
        }
    }
}
