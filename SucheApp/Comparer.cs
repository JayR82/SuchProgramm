using System;
using System.Text.RegularExpressions;

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
