using EduQuiz.DomainEntities.Domain;
using EduQuiz.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Implementation
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
      

        public ResultRepository(ApplicationDbContext context) : base(context)
        {
          
        }

        public async Task<Result> GetById(Guid id)
        {
            return await _entities.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Result>> GetResultsByUser(string userId)
        {
            return await _entities.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<int> GetAttemptCount(string userId, Guid quizId)
        {
            return await _entities.Where(r => r.UserId == userId && r.QuizId == quizId).CountAsync();
        }

     
    }
}