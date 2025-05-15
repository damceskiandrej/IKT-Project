using EduQuiz.DomainEntities.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EduQuiz.Service.BackgroundWorker
{
    public class QuizProcessingQueue : IQuizProcessingQueue
    {
        private readonly Channel<QuizResultRequest> _queue = Channel.CreateUnbounded<QuizResultRequest>();

        public void Enqueue(QuizResultRequest request)
        {
            _queue.Writer.TryWrite(request);
        }

        public IAsyncEnumerable<QuizResultRequest> Dequeue(CancellationToken cancellationToken) =>
            _queue.Reader.ReadAllAsync(cancellationToken);
    }
}
