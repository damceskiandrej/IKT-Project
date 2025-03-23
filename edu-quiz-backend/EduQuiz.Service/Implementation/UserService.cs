using Microsoft.AspNetCore.Identity;
using EduQuiz.DomainEntities.DTO.Request;
using EduQuiz.DomainEntities.Identity;
using EduQuiz.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<EduQuizUser> _userManager;

        public UserService(UserManager<EduQuizUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> RegisterUser(UserRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return false;
            }        
            if (await _userManager.FindByNameAsync(request.Username) != null)
            {
                return false;
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
                var result = await  _userManager.CreateAsync(user, request.Password);
                return result.Succeeded;
            }
            catch(Exception e)
            {
                return false;
            }
               
        }
    }
}
