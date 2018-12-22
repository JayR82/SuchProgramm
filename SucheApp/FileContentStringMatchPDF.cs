using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;

namespace DateiSuche
{
    public static class FileContentStringMatchPDF
    {
        public static bool ReadFileCompateText(string path, string s)
        {

            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return Comparer.CheckTextIfMatch(s, text.ToString());
            }
        }
    }
}
