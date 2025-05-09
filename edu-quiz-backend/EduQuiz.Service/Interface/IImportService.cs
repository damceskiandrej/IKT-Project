using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Response;
using EduQuiz.DomainEntities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Interface
{
    public interface IImportService
    {
        Task<Quiz> GetQuizFromFile(string fileName);
        Task<List<UserResponse>> GetStudentsFromFile(string fileName);
    }
}
