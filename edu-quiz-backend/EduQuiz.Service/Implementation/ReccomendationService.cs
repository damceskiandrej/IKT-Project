using EduQuiz.DomainEntities.Domain;
using EduQuiz.Repository.Interface;
using EduQuiz.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Implementation
{
    public class ReccomendationService : IReccomendationService
    {
        private readonly IReccomendationRepository _reccomendationRepository;

        public ReccomendationService(IReccomendationRepository reccomendationRepository)
        {
            _reccomendationRepository = reccomendationRepository;
        }

        public async Task<List<Reccomendation>> GetReccomendationQuizId(Guid quizId)
        {
            return await _reccomendationRepository.GetReccomendationByQuizId(quizId);
        }
    }
}