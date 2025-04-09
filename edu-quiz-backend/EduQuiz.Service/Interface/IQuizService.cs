using EduQuiz.DomainEntities.DTO.Request;
using EduQuiz.DomainEntities.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Interface
{
    public interface IQuizService
    {
        Task<ResponseModel> InsertQuizzesForUser(string userId, List<string> quizIds);
        Task PopulateData(List<QuizRequest> quizRequests);
        Task PopulateDataPerCategory(List<QuizRequest> quizRequests);
    }
}
