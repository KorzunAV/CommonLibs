using OpenHtmlToPdf;
using System.IO;

namespace Converters.HtmlToPdf
{
    public class SelectPdfConverter
    {
        public static void Convert(string htmlString, string baseUrl, string outpath)
        {
            var result = Pdf.From(htmlString).Content();
            var outFile = new FileInfo(outpath);
            using (var s = outFile.Create())
            {
                s.Write(result, 0, result.Length);
            }

        }
    }
}
