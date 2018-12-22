using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DateiSuche
{
    class Comparer
    {
        public static bool CheckTextIfMatch(string sTextToSearch, string FileTextContent)
        {
            try
            {
                Regex r = new Regex(sTextToSearch, RegexOptions.IgnoreCase);
                Match m = r.Match(FileTextContent);
                if (m.Success)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
