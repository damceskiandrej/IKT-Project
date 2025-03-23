using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduQuiz.Service.Interface;

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
        public FileContentResult GeneratePdf()
        {
            var document = _exportService.GeneratePdf();
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            Response.ContentType = "application/pdf";
            Response.Headers.Append("content-length", stream.Length.ToString());
            byte[] bytes = stream.ToArray();
            stream.Close();


            return File(bytes, "application/pdf", "Invoice.pdf");
        }
    }
}
