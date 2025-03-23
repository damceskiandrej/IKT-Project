using Microsoft.EntityFrameworkCore;
using EduQuiz.DomainEntities.Domain;
using EduQuiz.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Implementation
{
    public class QuizRepository : Repository<Quiz>, IQuizRepoistory
    {
        public QuizRepository(ApplicationDbContext context) : base(context) { }
        public async Task<Quiz> GetQuizByQuestion(string question)
        {
            return await _entities.FirstOrDefaultAsync(i => i.Question.Equals(question));
        }
    }
}
