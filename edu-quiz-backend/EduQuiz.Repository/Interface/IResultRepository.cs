using EduQuiz.DomainEntities.Domain;

namespace EduQuiz.Repository.Interface;

public interface IResultRepository : IRepository<Result>
{
    Task<Result> GetById(Guid id);
    Task<List<Result>> GetResultsByUser(string userId);
    Task<int> GetAttemptCount(string userId, Guid quizId);
  
}