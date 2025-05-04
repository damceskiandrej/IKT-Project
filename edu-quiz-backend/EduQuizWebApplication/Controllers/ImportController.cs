using EduQuiz.DomainEntities.Domain;
using EduQuiz.Service.Interface;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EduQuizWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [HttpPost]
        public IActionResult ImportQuiz(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\Files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            var quiz = _importService.GetQuizFromFile(file.FileName);
            return Ok(quiz);

        }

        [HttpPost]
        public async Task<IActionResult> ImportStudents(IFormFile file)
        {
            string pathToUpload = Path.Combine(Directory.GetCurrentDirectory(), "Files", file.FileName);

            using (var fileStream = System.IO.File.Create(pathToUpload))
            {
                await file.CopyToAsync(fileStream);
            }

            var students = await _importService.GetStudentsFromFile(file.FileName);
            return Ok(students);
        }


    }
}
