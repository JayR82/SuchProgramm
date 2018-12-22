using Excel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace DateiSuche
{
    class FileContentStringMatchXLSX
    {
        public static bool ReadFileCompateText(string path, string s)
        {
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            Regex r = new Regex(s, RegexOptions.IgnoreCase);
            DataSet result = excelReader.AsDataSet();

            for (int i = 0; i < result.Tables.Count; i++)
            {
                for (int j = 0; j < result.Tables[i].Rows.Count; j++)
                {
                    object[] row = result.Tables[i].Rows[j].ItemArray;
                    foreach (object item in row)
                    {
                        Match m = r.Match(item.ToString());
                        if (m.Success)
                        {
                            excelReader.Close();
                            return true;
                        }
                    }
                }
            }
            excelReader.Close();
            return false;
        }
    }
}
