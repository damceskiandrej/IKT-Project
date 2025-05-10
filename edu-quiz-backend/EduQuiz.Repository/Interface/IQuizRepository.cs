using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Interface
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        Task<Quiz> GetQuizByCategory(string category);

        //Demo Purposes only
        Task<Quiz> GetQuizByQuestion(string question);
        Task<List<Quiz>> GetQuizzesByCategories(List<string> allUniqueCategories);
        Task<Quiz> GetById(Guid id);
        Task<List<QuizResponse>> GetAllMapped();
        Task<List<Quiz>> GetQuizzesByCategory(string category);
        Task<List<Quiz>> GetQuizzesByIds(List<Guid> ids);
       
    }
}
