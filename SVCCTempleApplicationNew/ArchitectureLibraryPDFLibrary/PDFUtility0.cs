using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
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
        public void CreatePDFFromHtml(string htmlContent, string pdfFullFileName, string execUniqueId)
        {
            StringReader stringReader = new StringReader(htmlContent);
            Rectangle rectangle = PageSize.A4;
            //Rectangle rectangle = new Rectangle(900, 1170); - This is for custom width and height
            Document pdfDocument = new Document(rectangle, 10f, 10f, 10f, 0f);
            FileStream fileStream = new FileStream(pdfFullFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, fileStream);
            pdfDocument.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, pdfDocument, stringReader);
            stringReader.Close();
            stringReader.Dispose();
            fileStream.Close();
            fileStream.Dispose();
            pdfWriter.Close();
            pdfWriter.Dispose();
            pdfDocument.Close();
            pdfDocument.Dispose();
        }
        public void ExtractPDFFieldsiTextSharp(string pdfInputFullFileName, string outputFullFileName)
        {
            int i;
            //string pdfText;
            PdfReader pdfReaderInstance;
            AcroFields pdfAcroFieldsInstance;
            StreamWriter streamWriter = new StreamWriter(outputFullFileName);
            //PdfDictionary pdfDictionaryInstance;
            //FdfWriter fdfWriterInstance;

            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@");
            pdfReaderInstance = new PdfReader(pdfInputFullFileName);
            pdfAcroFieldsInstance = pdfReaderInstance.AcroFields;
            i = 0;
            foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfAcroFieldsInstance.Fields)
            {
                i++;
                //pdfAcroFieldsInstance.GetListOptionDisplay(kvp.Key);
                streamWriter.WriteLine(i + "\t" + kvp.Key + "\t" + kvp.Value + "\t" + pdfAcroFieldsInstance.GetField(kvp.Key) + "\t Left=" + pdfAcroFieldsInstance.GetFieldPositions(kvp.Key)[0].position.Left + "\t Top=" + pdfAcroFieldsInstance.GetFieldPositions(kvp.Key)[0].position.Top + "\t FieldType=" + pdfAcroFieldsInstance.GetFieldType(kvp.Key));
                //pdfAcroFieldsInstance.ExportAsFdf(fdfWriterInstance);
            }
            streamWriter.Close();
            //for (int page = 1; page <= pdfReaderInstance.NumberOfPages; page++)
            //{
            //    pdfText = PdfTextExtractor.GetTextFromPage(pdfReaderInstance, page, new SimpleTextExtractionStrategy());
            //    pdfText = pdfText.Replace("\n", "\r\n");
            //    Console.WriteLine(pdfText);
            //}
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@");
        }
        public void PopulatePDF(string inputPDFFullFileName, string outputPDFFullFileName, Dictionary<string, string> pdfDataNamesAndValues)
        {
            PdfReader pdfReaderInstance = new PdfReader(inputPDFFullFileName);
            PdfReader.unethicalreading = true;
            PdfStamper pdfStamperInstance = new PdfStamper(pdfReaderInstance, new FileStream(outputPDFFullFileName, FileMode.Create));
            AcroFields pdfAcroFieldsInstance = pdfStamperInstance.AcroFields;

            foreach (var formFieldNameValue in pdfDataNamesAndValues)
            {
                if (formFieldNameValue.Value != "")
                {
                    pdfAcroFieldsInstance.SetField(formFieldNameValue.Key, formFieldNameValue.Value);
                }
                pdfAcroFieldsInstance.SetFieldProperty(formFieldNameValue.Key, "setfflags", PdfFormField.FF_READ_ONLY, null);
            }

            pdfStamperInstance.FormFlattening = false;
            pdfStamperInstance.Close();
            pdfReaderInstance.Close();
        }
        public void PopulatePDFFromTemplate(string templatePDFFullFileName, string outputPDFFullFileName, Dictionary<string, string> pdfDataNamesAndValues)
        {
            PdfReader pdfReader = new PdfReader(templatePDFFullFileName);
            PdfReader.unethicalreading = true;
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(outputPDFFullFileName, FileMode.Create));
            AcroFields pdfAcroFields = pdfStamper.AcroFields;

            foreach (var pdfDataNamesAndValue in pdfDataNamesAndValues)
            {
                if (pdfDataNamesAndValue.Value != "")
                {
                    pdfAcroFields.SetField(pdfDataNamesAndValue.Key, pdfDataNamesAndValue.Key);
                }
                pdfAcroFields.SetFieldProperty(pdfDataNamesAndValue.Key, "setfflags", PdfFormField.FF_READ_ONLY, null);
            }

            pdfStamper.FormFlattening = false;
            pdfStamper.Close();
            pdfReader.Close();
        }
        public void ExtractPDFMetaDataiTextSharp(string pdfInputFullFileName, string outputFullFileName)
        {
            int i;
            //string pdfText;
            PdfReader pdfReaderInstance;
            AcroFields pdfAcroFieldsInstance;
            StreamWriter streamWriter = new StreamWriter(outputFullFileName);
            //PdfDictionary pdfDictionaryInstance;
            //FdfWriter fdfWriterInstance;

            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@");
            pdfReaderInstance = new PdfReader(pdfInputFullFileName);
            pdfAcroFieldsInstance = pdfReaderInstance.AcroFields;
            i = 0;
            foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfAcroFieldsInstance.Fields)
            {
                i++;
                //pdfAcroFieldsInstance.GetListOptionDisplay(kvp.Key);
                streamWriter.WriteLine(i + "\t" + kvp.Key + "\t" + kvp.Value + "\t" + pdfAcroFieldsInstance.GetField(kvp.Key) + "\t Left=" + pdfAcroFieldsInstance.GetFieldPositions(kvp.Key)[0].position.Left + "\t Top=" + pdfAcroFieldsInstance.GetFieldPositions(kvp.Key)[0].position.Top + "\t FieldType=" + pdfAcroFieldsInstance.GetFieldType(kvp.Key));
                //pdfAcroFieldsInstance.ExportAsFdf(fdfWriterInstance);
            }
            streamWriter.Close();
            //for (int page = 1; page <= pdfReaderInstance.NumberOfPages; page++)
            //{
            //    pdfText = PdfTextExtractor.GetTextFromPage(pdfReaderInstance, page, new SimpleTextExtractionStrategy());
            //    pdfText = pdfText.Replace("\n", "\r\n");
            //    Console.WriteLine(pdfText);
            //}
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@");
        }
        public void PopulatePDFWithData(string inputPDFFullFileName, string outputPDFFullFileName, Dictionary<string, string> formFieldNamesValues)
        {
            PdfReader pdfReaderInstance = new PdfReader(inputPDFFullFileName);
            PdfReader.unethicalreading = true;
            PdfStamper pdfStamperInstance = new PdfStamper(pdfReaderInstance, new FileStream(outputPDFFullFileName, FileMode.Create));
            AcroFields pdfAcroFieldsInstance = pdfStamperInstance.AcroFields;

            foreach (var formFieldNameValue in formFieldNamesValues)
            {
                if (formFieldNameValue.Value != "")
                {
                    pdfAcroFieldsInstance.SetField(formFieldNameValue.Key, formFieldNameValue.Value);
                }
                pdfAcroFieldsInstance.SetFieldProperty(formFieldNameValue.Key, "setfflags", PdfFormField.FF_READ_ONLY, null);
            }

            pdfStamperInstance.FormFlattening = false;
            pdfStamperInstance.Close();
            pdfReaderInstance.Close();
        }
    }
}
