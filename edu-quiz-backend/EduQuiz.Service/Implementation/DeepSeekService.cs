using EduQuiz.Repository.Interface;
using EduQuiz.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Implementation
{
    public class DeepSeekService : IDeepSeekService
    {
        private readonly IDeepSeekRepository _deepSeekRepository;

        public DeepSeekService(IDeepSeekRepository deepSeekRepository)
        {
            _deepSeekRepository = deepSeekRepository;
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            return await _deepSeekRepository.GenerateResponseAsync(prompt);
        }
    }
}
