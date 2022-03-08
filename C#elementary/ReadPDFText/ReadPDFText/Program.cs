using System;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

// https://stackoverflow.com/questions/83152/reading-pdf-documents-in-net


namespace ReadPDFText // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //iText7
            Console.WriteLine(
                ReadFile($@"{Environment.CurrentDirectory.Split(new String[] { "bin" },StringSplitOptions.None)[0]}\test.pdf"));
            
            
            
            
            
            
            Console.ReadLine();
        }

        /// <summary>
        /// iText7
        /// </summary>
        static string ReadFile(string pdfPath)
        {
            var pageText = new StringBuilder();
            using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(pdfPath)))
            {
                var pageNumbers = pdfDocument.GetNumberOfPages();
                for (int i = 1; i <= pageNumbers; i++)
                {
                    LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    PdfCanvasProcessor parser = new PdfCanvasProcessor(strategy);
                    parser.ProcessPageContent(pdfDocument.GetFirstPage());
                    pageText.Append(strategy.GetResultantText());
                }
            }
            return pageText.ToString();
        }
    }
}
