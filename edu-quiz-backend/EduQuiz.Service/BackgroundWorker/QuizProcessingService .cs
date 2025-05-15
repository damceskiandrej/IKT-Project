using EduQuiz.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.BackgroundWorker
{
    public class QuizProcessingService : BackgroundService
    {
        private readonly IQuizProcessingQueue _queue;
        private readonly IServiceProvider _services;

        public QuizProcessingService(IQuizProcessingQueue queue, IServiceProvider services)
        {
            _queue = queue;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var request in _queue.Dequeue(stoppingToken))
            {
                using var scope = _services.CreateScope();
                var resultService = scope.ServiceProvider.GetRequiredService<IResultService>();

                try
                {
                    await resultService.ProcessResult(request);
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }
}
