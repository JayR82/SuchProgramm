using CSharpJExcel.Jxl;
using System.Text.RegularExpressions;

namespace DateiSuche
{
    class FileContentStringMatchXLS
    {
        public static bool ReadFileCompateText(string path, string s)
        {
            Workbook workbook = Workbook.getWorkbook(new System.IO.FileInfo(path));
            int sheets = workbook.getNumberOfSheets();
            Regex r = new Regex(s, RegexOptions.IgnoreCase);
            for (int i = 0; i < sheets; i++)
            {
                var sheet = workbook.getSheet(i);
                CSharpJExcel.Jxl.Cell CellContent = sheet.findCell(r, 0, 0, sheet.getColumns(), sheet.getRows(), false);
                if (CellContent == null)
                { }
                else
                {
                    workbook.close();
                    return true;
                }
            }
            workbook.close();
            return false;
        }
    }
}
