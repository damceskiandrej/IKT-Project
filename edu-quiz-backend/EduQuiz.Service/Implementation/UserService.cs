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
using EduQuiz.DomainEntities.Roles;

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
            result.UserName = user.UserName ?? "";
            result.FirstName = user.FirstName;  
            result.LastName = user.LastName;    
            result.Email = user.Email;
            result.UserId = user.Id;
            result.IsSuccess = true;
            result.Role = user.Role;
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
                var role = isProfessor(request.Email) ? EduQuizRole.PROFESSOR : EduQuizRole.STUDENT;
                user.Role = role;

                var userCreated = await  _userManager.CreateAsync(user, request.Password);
                await _userManager.AddToRoleAsync(user, role.ToString());

                if (!userCreated.Succeeded)
                {
                    var errors = String.Join(", ", userCreated.Errors.Select(x => x.Description)).ToString();
                    result.Message = errors;
                    result.IsSuccess = false;
                    return result;
                }


                result.Message = "User Created";
                result.UserName = user.UserName;
                result.UserId = user.Id;
                result.IsSuccess = true;
                result.Role = role;
                return result;

            }
            catch(Exception e)
            {
                result.Message = $"Exception - {e.Message}";
                result.IsSuccess = false;
                return result;
            }
               
        }
        private bool isProfessor(string email)
        {
            return email.Contains("professor");
        }

        public async Task<UserResponse> GetUserProfile(string userId)
        {
            var result = new UserResponse();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                result.Message = "User not found";
                result.IsSuccess = false;
                return result;
            }

            result.IsSuccess = true;
            result.UserId = user.Id;
            result.UserName = user.UserName;
            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.Email = user.Email;
            
            return result;
        }

        public async Task<UserResponse> UpdateUserProfile(string userId, UserUpdateRequest request)
        {
            var result = new UserResponse();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                result.Message = "User not found";
                result.IsSuccess = false;
                return result;
            }

            // Check if email is already taken by another user
            if (user.Email != request.Email)
            {
                var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
                if (userWithEmail != null && userWithEmail.Id != userId)
                {
                    result.Message = "Email already taken";
                    result.IsSuccess = false;
                    return result;
                }
            }

            // Check if username is already taken by another user
            if (user.UserName != request.UserName)
            {
                var userWithUsername = await _userManager.FindByNameAsync(request.UserName);
                if (userWithUsername != null && userWithUsername.Id != userId)
                {
                    result.Message = "Username already taken";
                    result.IsSuccess = false;
                    return result;
                }
            }

            // Update user profile
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.UserName = request.UserName;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                result.Message = "Failed to update user profile";
                result.IsSuccess = false;
                return result;
            }

            result.Message = "Profile updated successfully";
            result.UserName = user.UserName;
            result.UserId = user.Id;
            result.IsSuccess = true;
            return result;
        }

    }
}
