using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryPDFLibrary
{
    public partial class PDFUtility
    {
        public void CreatePDFFileFromHtml(string htmlContent, string pdfOutputFullFileName, string execUniqueId)
        {
            using (FileStream fileStream = new FileStream(pdfOutputFullFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                HtmlConverter.ConvertToPdf(htmlContent, fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }
        }
        /*
        //https://kb.itextpdf.com/home/it7kb/ebooks/itext-7-converting-html-to-pdf-with-pdfhtml/chapter-1-hello-html-to-pdf
        public void CreatePDF(string outputPDFFullFileName)
        {
            PdfWriter pdfWriter = new PdfWriter(outputPDFFullFileName);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document document = new Document(pdfDocument);
            Paragraph header = new Paragraph("A1 TRUCK")
               .SetBold()
               //.SetFontFamily("Arial")
               .SetFontSize(22.3f)
               //.SetPaddingLeft(50)
               .SetPaddingTop(100)
               .SetTextAlignment(TextAlignment.CENTER)
               ;

            document.Add(header);
            document.Close();
        }
        public void CreatePDFTest()
        {
            // Must have write permissions to the path folder
            PdfWriter writer = new PdfWriter(@"C:\Ravi\Dev\SchoolPrd\Docs\Catalog_2018_iText7_Test.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Header
            Paragraph header = new Paragraph("HEADER")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            // New line
            Paragraph newline = new Paragraph(new Text("\n"));

            document.Add(newline);
            document.Add(header);

            // Add sub-header
            Paragraph subheader = new Paragraph("SUB HEADER")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(15);
            document.Add(subheader);

            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // Add paragraph1
            Paragraph paragraph1 = new Paragraph("Lorem ipsum " +
               "dolor sit amet, consectetur adipiscing elit, " +
               "sed do eiusmod tempor incididunt ut labore " +
               "et dolore magna aliqua.");
            document.Add(paragraph1);

            // Add image
            Image img = new Image(ImageDataFactory
               .Create(@"C:\Ravi\Dev\SchoolPrd\Docs\PDF10.jpg"))
               .SetTextAlignment(TextAlignment.CENTER);
            document.Add(img);

            // Table
            Table table = new Table(2, false);
            Cell cell11 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("State"));
            Cell cell12 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Capital"));

            Cell cell21 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("New York"));
            Cell cell22 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Albany"));

            Cell cell31 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("New Jersey"));
            Cell cell32 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Trenton"));

            Cell cell41 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("California"));
            Cell cell42 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Sacramento"));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell31);
            table.AddCell(cell32);
            table.AddCell(cell41);
            table.AddCell(cell42);

            document.Add(newline);
            document.Add(table);

            // Hyper link
            Link link = new Link("click here",
               PdfAction.CreateURI("https://www.google.com"));
            Paragraph hyperLink = new Paragraph("Please ")
               .Add(link.SetBold().SetUnderline()
               .SetItalic().SetFontColor(ColorConstants.BLUE))
               .Add(" to go www.google.com.");

            document.Add(newline);
            document.Add(hyperLink);

            // Page numbers
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format("page" + i + " of " + n)),
                   559, 806, i, TextAlignment.RIGHT,
                   VerticalAlignment.TOP, 0);
            }

            // Close document
            document.Close();
        }
        //public void Test()
        //{
        //    var htmlSource = "input.html";
        //    var pdfDest = "output.pdf";
        //    // pdfHTML specific code
        //    //ConverterProperties converterProperties = new ConverterProperties();
        //    HtmlConverter.convertToPdf(new File(htmlSource), new FileOutputStream(pdfDest));
        //}
        //public void generatePDF(String htmlFile)
        //{
        //    try
        //    {

        //        //HTML String
        //        String htmlString = htmlFile;
        //        //Setting destination 
        //        FileOutputStream fileOutputStream = new FileOutputStream(new File(dirPath + "/USER-16-PF-Report.pdf"));

        //        PdfWriter pdfWriter = new PdfWriter(fileOutputStream);
        //        ConverterProperties converterProperties = new ConverterProperties();
        //        PdfDocument pdfDocument = new PdfDocument(pdfWriter);

        //        //For setting the PAGE SIZE
        //        pdfDocument.setDefaultPageSize(new PageSize(PageSize.A3));

        //        Document document = HtmlConverter.convertToDocument(htmlFile, pdfDocument, converterProperties);
        //        document.close();
        //    }
        //    catch (Exception e)
        //    {
        //        //e.printStackTrace();
        //    }
        //}
        */
    }
}
