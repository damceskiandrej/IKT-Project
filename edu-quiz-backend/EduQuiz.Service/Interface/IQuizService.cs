using EduQuiz.DomainEntities.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Interface
{
    public interface IQuizService
    {
        Task PopulateData(List<QuizRequest> quizRequests);
        Task PopulateDataPerCategory(List<QuizRequest> quizRequests);
    }
}
