using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using Studio36.DTOs;
using Studio36.ReportComponent.Interfaces;

namespace Studio36.ReportComponent
{
    public class PdfReportGenerator : IReportGenerator
    {
        private readonly string outputDirectory;

        static PdfReportGenerator()
        {
            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = new Studio36FontResolver();
            }
        }

        public PdfReportGenerator(string outputDirectory = "Reports")
        {
            this.outputDirectory = outputDirectory;
        }

        public ReportResultData GenerateProjectReport(ProjectReportData data)
        {
            Directory.CreateDirectory(outputDirectory);

            string fileName = $"project-{data.IdProjeto}-report.pdf";
            string filePath = Path.Combine(outputDirectory, fileName);

            using PdfDocument document = new();
            document.Info.Title = $"Project {data.IdProjeto} Report";

            PdfPage page = document.AddPage();
            using XGraphics graphics = XGraphics.FromPdfPage(page);

            XFont titleFont = new(Studio36FontResolver.FontFamilyName, 18, XFontStyleEx.Bold);
            XFont headingFont = new(Studio36FontResolver.FontFamilyName, 12, XFontStyleEx.Bold);
            XFont bodyFont = new(Studio36FontResolver.FontFamilyName, 11, XFontStyleEx.Regular);

            double y = 50;

            graphics.DrawString("Studio 36 - Project Report", titleFont, XBrushes.Black, new XRect(50, y, page.Width.Point - 100, 30), XStringFormats.TopLeft);
            y += 45;

            DrawLine(graphics, bodyFont, $"Project ID: {data.IdProjeto}", ref y);
            DrawLine(graphics, bodyFont, $"Name: {data.Nome}", ref y);
            DrawLine(graphics, bodyFont, $"Description: {data.Descricao}", ref y);
            DrawLine(graphics, bodyFont, $"Start date: {data.DataInicio:yyyy-MM-dd}", ref y);
            DrawLine(graphics, bodyFont, $"End date: {data.DataFim:yyyy-MM-dd}", ref y);

            y += 15;
            DrawLine(graphics, headingFont, "Tasks:", ref y);

            if (data.Tarefas.Count == 0)
            {
                DrawLine(graphics, bodyFont, "- No tasks associated with this project.", ref y);
            }
            else
            {
                foreach (string tarefa in data.Tarefas)
                {
                    DrawLine(graphics, bodyFont, $"- {tarefa}", ref y);
                }
            }

            document.Save(filePath);

            return new ReportResultData(
                true,
                filePath,
                $"Report generated successfully: {filePath}");
        }

        private static void DrawLine(XGraphics graphics, XFont font, string text, ref double y)
        {
            graphics.DrawString(text, font, XBrushes.Black, new XRect(50, y, 500, 20), XStringFormats.TopLeft);
            y += 22;
        }
    }
}
