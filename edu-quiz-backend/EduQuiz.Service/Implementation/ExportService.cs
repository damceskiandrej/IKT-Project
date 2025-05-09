using Microsoft.SqlServer.Server;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Fields;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using EduQuiz.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using EduQuiz.DomainEntities.DTO.Request;
using Microsoft.AspNetCore.Identity;
using EduQuiz.DomainEntities.Identity;

namespace EduQuiz.Service.Implementation
{
    public class ExportService : IExportService
    {
        private readonly IQuizService _quizService;
        private readonly UserManager<EduQuizUser> _userManager;

        public ExportService(IQuizService quizService, UserManager<EduQuizUser> userManager)
        {
            _quizService = quizService;
            _userManager = userManager;
        }

        public async Task<PdfDocument> GeneratePdf(PdfRequest request)
        {
            try
            {
                var document = new Document();
                var quizSummary = await _quizService.GetQuizSummaryAsync(request.QuizId);
                var qaDict = FormatQuizSummary(quizSummary);
                var user = await _userManager.FindByIdAsync(request.UserId);
                //TODO: Quiz Summary for Quiz... 
                //user.Quizzes.Where(i => i.Id.Equals(request.QuizId)).FirstOrDefault();
                //TODO: Add validation
                BuildDocument(document, qaDict, user);

                var pdfRenderer = new PdfDocumentRenderer();
                pdfRenderer.Document = document;
                pdfRenderer.RenderDocument();

                return pdfRenderer.PdfDocument;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private Dictionary<string, string> FormatQuizSummary(string quizSummary)
        {
            string[] parts = quizSummary.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var qaDict = new Dictionary<string, string>();

            foreach (var part in parts)
            {
                string[] split = part.Split(new[] { ':' }, 2);
                if (split.Length == 2)
                {
                    string question = split[0].Trim();
                    string answer = split[1].Trim();
                    qaDict[question] = answer;
                }
            }

            return qaDict;

        }

        private void BuildDocument(Document document, Dictionary<string, string> qaDict, EduQuizUser user)
        {
            Section section = document.AddSection();

            AddHeader(section, user);
            section.AddParagraph().Format.SpaceAfter = 10;

            // Placeholder for quiz review 

            section.AddParagraph().Format.SpaceBefore = 15;
            AddQuizSummarylHeader(section);
            AddSummaryBody(section, qaDict);

            AddFooter(section);
        }

        private void AddHeader(Section section, EduQuizUser user)
        {
            var headerTable = section.AddTable();
            headerTable.Borders.Width = 0;
            headerTable.AddColumn("10cm");
            headerTable.AddColumn("6cm");

            var headerRow = headerTable.AddRow();

            var leftCell = headerRow.Cells[0];
            var leftParagraph = leftCell.AddParagraph();
            leftParagraph.AddFormattedText("IKT Team 40", TextFormat.Bold);
            leftParagraph.Format.Font.Size = 14;
            leftParagraph.Format.SpaceAfter = 5;

            var reportTitle = leftCell.AddParagraph("Report For");
            reportTitle.Format.SpaceBefore = "0.5cm";
            reportTitle.Format.Font.Size = 12;
            reportTitle.Format.Font.Bold = true;
            reportTitle.Format.SpaceAfter = "0.2cm";

            // Full Name
            var namePara = leftCell.AddParagraph();
            namePara.AddFormattedText("Full name: ", TextFormat.Bold);
            namePara.AddText($"{user.FirstName} {user.LastName}");

            // Username
            var usernamePara = leftCell.AddParagraph();
            usernamePara.AddFormattedText("Username: ", TextFormat.Bold);
            usernamePara.AddText(user.UserName);

            // Email
            var emailPara = leftCell.AddParagraph();
            emailPara.AddFormattedText("Email: ", TextFormat.Bold);
            emailPara.AddText(user.Email);


            var rightCell = headerRow.Cells[1];
            var logoParagraph = rightCell.AddParagraph();
            logoParagraph.Format.Alignment = ParagraphAlignment.Right;
            logoParagraph.AddImage("Files/authentication.png").Width = "4cm";

            var dateParagraph = rightCell.AddParagraph();
            dateParagraph.Format.Alignment = ParagraphAlignment.Right;
            dateParagraph.AddLineBreak();
            dateParagraph.AddText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        private void AddQuizSummarylHeader(Section section)
        {
            var summaryParagraph = section.AddParagraph();
            summaryParagraph.Format.Font.Size = 12;
            summaryParagraph.Format.Font.Bold = true;
            summaryParagraph.Format.Alignment = ParagraphAlignment.Center;
            summaryParagraph.AddText("Quiz Summary");
        }

        private void AddSummaryBody(Section section, Dictionary<string, string> qaDict)
        {
            var summaryBody = section.AddParagraph();
            summaryBody.Format.SpaceAfter = "1cm";
            summaryBody.Format.Font.Size = 14;

            foreach (var entry in qaDict)
            {
                var entryParagraph = section.AddParagraph();
                entryParagraph.Format.SpaceAfter = "0.5cm";
                entryParagraph.Format.Font.Size = 12;

                entryParagraph.AddFormattedText($"{entry.Key}: ", TextFormat.Bold);
                entryParagraph.AddText(entry.Value);
            }
        }

        private void AddFooter(Section section)
        {
            var footer = section.Footers.Primary.AddParagraph();
            footer.AddText("Quiz Report - IKT Team 40");
            footer.Format.Alignment = ParagraphAlignment.Center;
            footer.Format.Font.Size = 10;
            footer.Format.Font.Color = Colors.Gray;
        }




    }
}