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
    public class QuizRepository : Repository<Quiz>, IQuizRepository
    {
        public QuizRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Quiz> GetQuizByCategory(string category)
        {
            return await _entities.FirstOrDefaultAsync(q => q.Category.Equals(category));
        }

        public async Task<Quiz> GetQuizByQuestion(string question)
        {
            return await _entities.FirstOrDefaultAsync(i => i.Title.Equals(question));
        }

        public async Task<List<Quiz>> GetQuizzesByCategories(List<string> allUniqueCategories)
        {
            return await _entities.Where(x => allUniqueCategories.Contains(x.Category)).ToListAsync();
        }
        
        public async Task<Quiz> GetById(Guid id)
        {
            return await _entities
        .Include(q => q.Questions) 
        .ThenInclude(q => q.Answers) 
        .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<Quiz>> GetAll()
        {
            return await _entities
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .ToListAsync();
        }

        public async Task<List<Quiz>> GetQuizzesByCategory(string category)
        {
            return await _entities
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .Where(q => q.Category == category)
                .ToListAsync();
        }

        public async Task<List<Quiz>> GetQuizzesByIds(List<Guid> ids)
        {
            return await _entities
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .Where(q => ids.Contains(q.Id))
                .ToListAsync();
        }

        public async Task<List<Quiz>> GetQuizzesByIds(List<string> quizIds)
        {
            var quizIdsParsed = quizIds.Select(Guid.Parse).ToList();
            return await _entities.Where(x => quizIdsParsed.Contains(x.Id)).ToListAsync();
        }
    }
}
