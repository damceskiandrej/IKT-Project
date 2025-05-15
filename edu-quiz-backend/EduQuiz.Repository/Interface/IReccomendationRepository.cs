using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Interface
{
    public interface IReccomendationRepository
    {
        Task<List<Reccomendation>> GetReccomendationByQuizId(Guid quizId);
        Task<List<QuizExplanationResponse>> GetReccomendationByQuizIdAndUserId(Guid quizId, string userId);
        void InsertReccomendation(Reccomendation reccomendation);
    }
}