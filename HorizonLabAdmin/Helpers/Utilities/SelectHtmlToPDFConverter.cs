using HorizonLabAdmin.Interfaces;
using Microsoft.Extensions.Logging;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class SelectHtmlToPDFConverter : ISelectHtmlToPDFConverter
    {
        private readonly ILogger<SelectHtmlToPDFConverter> _logger;

        public SelectHtmlToPDFConverter(ILogger<SelectHtmlToPDFConverter> logger)
        {
            _logger = logger;
        }

        public MemoryStream ConvertHtmlURLToPDFMemoryStream(string URL)
        {
            try
            {
                HtmlToPdf htmltopdf = new HtmlToPdf();
                htmltopdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                htmltopdf.Options.PdfPageSize = PdfPageSize.Letter;
                htmltopdf.Options.MarginTop = 25;
                htmltopdf.Options.MarginLeft = 3;
                htmltopdf.Options.MarginRight = 3;
                //htmltopdf.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
                //htmltopdf.Options.AutoFitHeight= HtmlToPdfPageFitMode.ShrinkOnly;
                htmltopdf.Options.WebPageWidth = 760;
                htmltopdf.Options.WebPageHeight = 500;
                //htmltopdf.Options.WebPageWidth = 696;
                //htmltopdf.Options.WebPageHeight = 1050;                

                PdfDocument pdfDocument = htmltopdf.ConvertUrl(URL);
                byte[] pdf = pdfDocument.Save();
                //convert to memory stream
                MemoryStream stream = new MemoryStream(pdf);
                pdfDocument.Close();
                return stream;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HtmlToPDF > ConvertHtmlURLToPDFMemoryStream() : {exc.Message} | URL:{URL}");
                throw exc.InnerException;
            }
        }
    }
}
