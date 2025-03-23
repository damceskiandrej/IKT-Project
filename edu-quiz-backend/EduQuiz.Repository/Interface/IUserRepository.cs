using EduQuiz.DomainEntities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<EduQuizUser> GetAll();
        EduQuizUser Get(string id);
        void Insert(EduQuizUser entity);
        void Update(EduQuizUser entity);
        void Delete(EduQuizUser entity);
    }
}
