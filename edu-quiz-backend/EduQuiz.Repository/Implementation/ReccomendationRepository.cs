using EduQuiz.DomainEntities.Domain;
using EduQuiz.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Implementation
{
    public class ReccomendationRepository : Repository<Reccomendation>, IReccomendationRepository
    {
        public ReccomendationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Reccomendation>> GetReccomendationByUserIdAndQuizId(string userId, Guid quizId)
        {
            return await _entities.Where(x => x.QuizId.Equals(quizId) && x.UserId.Equals(userId)).ToListAsync();
        }

        public void InsertReccomendation(Reccomendation reccomendation)
        {
           _entities.Add(reccomendation);
        }
    }
}
