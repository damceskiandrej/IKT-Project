using Microsoft.AspNetCore.Identity;
using StudyBuddy.DomainEntities.DTO.Request;
using StudyBuddy.DomainEntities.Identity;
using StudyBuddy.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<StudyBuddyUser> _userManager;

        public UserService(UserManager<StudyBuddyUser> userManager)
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
                var user = new StudyBuddyUser()
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
