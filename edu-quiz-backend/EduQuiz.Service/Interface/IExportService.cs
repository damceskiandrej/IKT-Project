using EduQuiz.DomainEntities.DTO.Request;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Service.Interface
{
    public interface IExportService
    {
        Task<PdfDocument> GeneratePdf(PdfRequest request);
    }
}
