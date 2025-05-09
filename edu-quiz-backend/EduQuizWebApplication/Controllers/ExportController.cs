using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduQuiz.Service.Interface;
using EduQuiz.DomainEntities.DTO.Request;

namespace EduQuizWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpPost]
        public async Task<FileContentResult> GeneratePdf([FromBody] PdfRequest request)
        {
            var document = await _exportService.GeneratePdf(request);
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            Response.ContentType = "application/pdf";
            Response.Headers.Append("content-length", stream.Length.ToString());
            byte[] bytes = stream.ToArray();
            stream.Close();


            return File(bytes, "application/pdf", "QuizSummaryReport.pdf");
        }
    }
}