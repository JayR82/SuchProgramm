using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateiSuche
{
    class FileContentStringMatchTXT
    {
        public static bool ReadFileCompateText(string path, string s)
        {
            string content = System.IO.File.ReadAllText(path);
            return Comparer.CheckTextIfMatch(s, content);
        }

    }
}
