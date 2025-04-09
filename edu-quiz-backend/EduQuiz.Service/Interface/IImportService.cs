using EduQuiz.DomainEntities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Interface
{
    public interface IImportService
    {
        Task<Quiz> GetQuizFromFile(string fileName);
    }
}
