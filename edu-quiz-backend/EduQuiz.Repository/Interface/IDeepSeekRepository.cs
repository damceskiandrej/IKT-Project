using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Interface
{
    public interface IDeepSeekRepository
    {
        Task<string> GenerateResponseAsync(string prompt);
    }
}
