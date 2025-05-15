using EduQuiz.DomainEntities.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.BackgroundWorker
{
    public interface IQuizProcessingQueue
    {
        void Enqueue(QuizResultRequest request);
        IAsyncEnumerable<QuizResultRequest> Dequeue(CancellationToken cancellationToken);
    }
}
