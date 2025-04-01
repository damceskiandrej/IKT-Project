using EduQuiz.DomainEntities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Interface
{
    public interface IQuizRepoistory : IRepository<Quiz>
    {
        Task<Quiz> GetQuizByCategory(string category);

        //Demo Purposes only
        Task<Quiz> GetQuizByQuestion(string question);
        Task<List<Quiz>> GetQuizzesByCategories(List<string> allUniqueCategories);
    }
}
