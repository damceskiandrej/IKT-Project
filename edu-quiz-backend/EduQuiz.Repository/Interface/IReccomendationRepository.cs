using EduQuiz.DomainEntities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Interface
{
    public interface IReccomendationRepository
    {
        Task<List<Reccomendation>> GetReccomendationByUserIdAndQuizId(string userId, Guid quizId);
        void InsertReccomendation(Reccomendation reccomendation);
    }
}
