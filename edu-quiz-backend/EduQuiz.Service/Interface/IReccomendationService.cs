using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Interface
{
    public interface IReccomendationService
    {
        Task<List<Reccomendation>> GetReccomendationQuizId(Guid quizId);
        Task<List<QuizExplanationResponse>> GetReccomendationQuizIdAndUserId(Guid quizId, string userId);
    }
}