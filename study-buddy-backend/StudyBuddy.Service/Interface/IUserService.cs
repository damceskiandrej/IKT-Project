using StudyBuddy.DomainEntities.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Service.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserRequest request);
    }
}
