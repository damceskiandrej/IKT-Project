using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Response;
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

        public async Task<List<Reccomendation>> GetReccomendationByQuizId(Guid quizId)
        {
            return await _entities.Where(x => x.QuizId.Equals(quizId)).ToListAsync();
        }

        public async Task<List<QuizExplanationResponse>> GetReccomendationByQuizIdAndUserId(Guid quizId, string userId)
        {
            return await _entities
                .Where(x => x.QuizId.Equals(quizId) && x.UserId.Equals(userId))
                .Select(x => new QuizExplanationResponse {
                    Explanation = x.Explanation,
                    Question = x.Question,
                    IsProcessed = x.IsProcessed
                })
                .ToListAsync();
        }

        public void InsertReccomendation(Reccomendation reccomendation)
        {
            _entities.Add(reccomendation);
        }
    }
}