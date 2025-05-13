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
using EduQuiz.DomainEntities.DTO.Response;

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
                //var quizSummary = "";
                //var qaDict = FormatQuizSummary(quizSummary);
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new Exception("User not Found");
                }
                var result = await _quizService.GetQuizByUser(request.UserId, request.QuizId);
                //TODO: Quiz Summary for Quiz... 
                //user.Quizzes.Where(i => i.Id.Equals(request.QuizId)).FirstOrDefault();
                //TODO: Add validation
                BuildDocument(document, quizSummary, user, result);

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

        private void BuildDocument(Document document, List<QuizExplanationResponse> quizSummary, EduQuizUser user, QuizWithResultsResponse result)
        {
            Section section = document.AddSection();

            AddHeader(section, user);
            section.AddParagraph().Format.SpaceAfter = 10;

            AddQuizReviewSection(section, result);
            section.AddParagraph().Format.SpaceAfter = 10;

            section.AddParagraph().Format.SpaceBefore = 15;
            AddQuizSummarylHeader(section);
            AddSummaryBody(section, quizSummary);

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

        private void AddQuizReviewSection(Section section, QuizWithResultsResponse result)
        {
            var lastResult = result.Results.OrderByDescending(r => r.NumberOfAttempts).FirstOrDefault();
            if (lastResult == null) return;

            var userAnswersMap = lastResult.UserAnswers.ToDictionary(ua => ua.QuestionId, ua => ua.SelectedAnswerIds.ToHashSet());

            foreach (var question in result.Quiz.Questions)
            {
                // Add question text
                var questionParagraph = section.AddParagraph($"Q: {question.QuestionText}");
                questionParagraph.Format.Font.Bold = true;
                questionParagraph.Format.SpaceBefore = 10;
                questionParagraph.Format.SpaceAfter = 5;

                var userSelectedIds = userAnswersMap.TryGetValue(question.QuestionId, out var selected)
                    ? selected.ToHashSet()
                    : new HashSet<Guid>();

                string tickPath = "Files/check.png";
                string crossPath = "Files/cross.png";

                foreach (var answer in question.Answers)
                {
                    if (answer.AnswerText.Contains("n/a"))
                    {
                        continue;
                    }
                    var isSelected = userSelectedIds.Contains(answer.AnswerId);
                    var isCorrect = answer.IsCorrect;

                    var para = section.AddParagraph();
                    para.Format.LeftIndent = "1cm";

                    // Add image (new instance every time)
                    string imagePath = isSelected
                        ? (isCorrect ? tickPath : crossPath)
                        : null;

                    if (imagePath != null)
                    {
                        var img = para.AddImage(imagePath); // New image per paragraph
                        img.Width = "0.4cm";
                        img.LockAspectRatio = true;
                        para.AddSpace(1);
                    }

                    para.AddText(answer.AnswerText);

                    // Optional background color
                    if (isCorrect)
                    {
                        para.Format.Shading.Color = Colors.LightGreen;
                    }
                }
            }
        }


        private void AddQuizSummarylHeader(Section section)
        {
            var summaryParagraph = section.AddParagraph();
            summaryParagraph.Format.Font.Size = 12;
            summaryParagraph.Format.Font.Bold = true;
            summaryParagraph.Format.Alignment = ParagraphAlignment.Center;
            summaryParagraph.AddText("Quiz Summary");
        }

        private void AddSummaryBody(Section section, List<QuizExplanationResponse> quizSummary)
        {
            var summaryBody = section.AddParagraph();
            summaryBody.Format.SpaceAfter = "1cm";
            summaryBody.Format.Font.Size = 14;

            foreach (var explaination in quizSummary)
            {
                var entryParagraph = section.AddParagraph();
                entryParagraph.Format.SpaceAfter = "0.5cm";
                entryParagraph.Format.Font.Size = 12;

                entryParagraph.AddFormattedText($"{explaination.Question}: ", TextFormat.Bold);
                entryParagraph.AddText(explaination.Explanation);
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