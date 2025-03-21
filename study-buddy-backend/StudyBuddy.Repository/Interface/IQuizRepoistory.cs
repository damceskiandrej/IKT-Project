using StudyBuddy.DomainEntities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Repository.Interface
{
    public interface IQuizRepoistory : IRepository<Quiz>
    {
        //Demo Purposes only
        Task<Quiz> GetQuizByQuestion(string question);
    }
}
