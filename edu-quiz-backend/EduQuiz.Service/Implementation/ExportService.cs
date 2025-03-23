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

namespace EduQuiz.Service.Implementation
{
    public class ExportService : IExportService
    {
        public PdfDocument GeneratePdf()
        {
            try
            {
                var document = new Document();

                BuildDocument(document);

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

        private async Task BuildDocument(Document document)
        {
            Section section = document.AddSection();

            // Create a Table for Header
            var headerTable = section.AddTable();
            headerTable.Borders.Width = 0; // No borders for a clean look
            headerTable.AddColumn("10cm"); // Left Section
            headerTable.AddColumn("6cm");  // Right Section (for logo)

            var headerRow = headerTable.AddRow();

            // Left Section: Store Info
            var leftCell = headerRow.Cells[0];
            var leftParagraph = leftCell.AddParagraph();
            leftParagraph.AddFormattedText("BEST STORE", TextFormat.Bold);
            leftParagraph.Format.Font.Size = 14;
            leftParagraph.Format.SpaceAfter = 5;

            leftCell.AddParagraph("Website: www.beststore.com");
            leftCell.AddParagraph("Email: contact@beststore.com");
            leftCell.AddParagraph("Phone: +1-213-4567-890");

            leftCell.AddParagraph("\nBILLED TO");
            leftCell.AddParagraph("John Smith");
            leftCell.AddParagraph("201 Green Street");
            leftCell.AddParagraph("California, USA");
            leftCell.AddParagraph("Email: john@gmail.com");
            leftCell.AddParagraph("Phone: +1-321-7654-098");

            // Right Section: Logo + Invoice Details
            var rightCell = headerRow.Cells[1];
            var logoParagraph = rightCell.AddParagraph();
            logoParagraph.Format.Alignment = ParagraphAlignment.Right;
            logoParagraph.AddImage("path/to/logo.png").Width = "4cm"; // Adjust path & size

            var invoiceParagraph = rightCell.AddParagraph();
            invoiceParagraph.Format.Alignment = ParagraphAlignment.Right;
            invoiceParagraph.AddFormattedText("Invoice no: ", TextFormat.Bold);
            invoiceParagraph.AddText("N84759458");

            invoiceParagraph.AddLineBreak();
            invoiceParagraph.AddText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            // Add Space Before Table
            section.AddParagraph().Format.SpaceAfter = 10;

            // Create the Quiz Table (Already Implemented)
            var table = section.AddTable();
            table.Borders.Width = 0.75;
            table.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn("4cm");
            table.AddColumn("5cm");
            table.AddColumn("2cm");
            table.AddColumn("2cm");
            table.AddColumn("2cm");

            var headerRowQuiz = table.AddRow();
            headerRowQuiz.HeadingFormat = true;
            headerRowQuiz.Format.Font.Bold = true;
            headerRowQuiz.Shading.Color = Colors.LightGray;
            headerRowQuiz.Cells[0].AddParagraph("GUID");
            headerRowQuiz.Cells[1].AddParagraph("Quiz Name");
            headerRowQuiz.Cells[2].AddParagraph("Total Points");
            headerRowQuiz.Cells[3].AddParagraph("Percentage");
            headerRowQuiz.Cells[4].AddParagraph("Pass/Fail");

            var quizResults = new List<(string GUID, string QuizName, int TotalPoints, double Percentage)>
    {
        (Guid.NewGuid().ToString(), "Math Quiz", 100, 75.0),
        (Guid.NewGuid().ToString(), "Science Quiz", 100, 48.5),
        (Guid.NewGuid().ToString(), "History Quiz", 100, 92.3),
        (Guid.NewGuid().ToString(), "Geography Quiz", 100, 50.0)
    };

            var quizzesToRetake = new List<string>();


            foreach (var quiz in quizResults)
            {
                var row = table.AddRow();
                row.Cells[0].AddParagraph(quiz.GUID);
                row.Cells[1].AddParagraph(quiz.QuizName);
                row.Cells[2].AddParagraph(quiz.TotalPoints.ToString());
                row.Cells[3].AddParagraph(quiz.Percentage.ToString("0.0") + "%");
                row.Cells[4].AddParagraph(quiz.Percentage > 50 ? "Pass" : "Fail");

                if (quiz.Percentage <= 50) // Retake quizzes with <60% score
                {
                    quizzesToRetake.Add(quiz.QuizName);
                }
            }

            // Add Space Before Motivational Text
            section.AddParagraph().Format.SpaceBefore = 15;

            // Motivational Text
            var motivationParagraph = section.AddParagraph();
            motivationParagraph.Format.Font.Size = 12;
            motivationParagraph.Format.Font.Bold = true;
            motivationParagraph.Format.Alignment = ParagraphAlignment.Center;
            motivationParagraph.AddText("Keep Learning with Study Buddy!");

            var motivationBody = section.AddParagraph();
            motivationBody.Format.Font.Size = 11;
            motivationBody.Format.Alignment = ParagraphAlignment.Left;
            motivationBody.AddText("Your learning journey doesn't stop here! To improve your scores, we recommend retaking the following quizzes: ");

            if (quizzesToRetake.Any())
            {
                motivationBody.AddFormattedText(string.Join(", ", quizzesToRetake), TextFormat.Bold);
                motivationBody.AddLineBreak();
                motivationBody.Format.SpaceBefore = 10;
            }
            else
            {
                motivationBody.AddFormattedText("You're doing great! Keep practicing to master your knowledge!", TextFormat.Bold);
                motivationBody.AddLineBreak();
                motivationBody.Format.SpaceBefore = 10;
            }

            motivationBody.AddLineBreak();
            motivationBody.Format.SpaceBefore = 10;
            motivationBody.AddText("Study Buddy, our AI-powered tutor, can guide you through the material and help you improve. Access Study Buddy now on our website: ");

            var link = motivationBody.AddHyperlink("https://localhost:7130/home", HyperlinkType.Web);
            link.AddText("Study Buddy");

            // Footer
            var footer = section.Footers.Primary.AddParagraph();
            footer.AddText("Quiz Report - IKT Team 40");
            footer.Format.Alignment = ParagraphAlignment.Center;
            footer.Format.Font.Size = 10;
            footer.Format.Font.Color = Colors.Gray;
        }



    }
}
