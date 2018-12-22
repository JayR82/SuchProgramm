using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Text;

namespace DateiSuche
{
    class FileContentStringMatchODF
    {
        public static bool ReadFileCompateText(string path, string s)
        {
            var contentXml = "";
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);

            using (var zipInputStream = new ZipInputStream(stream))
            {
                ZipEntry contentEntry = null;
                while ((contentEntry = zipInputStream.GetNextEntry()) != null)
                {
                    if (!contentEntry.IsFile)
                        continue;
                    if (contentEntry.Name.ToLower() == "content.xml")
                        break;
                }

                if (contentEntry.Name.ToLower() != "content.xml")
                {
                    return false;
                }

                var bytesResult = new byte[] { };
                var bytes = new byte[2000];
                var i = 0;

                while ((i = zipInputStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    var arrayLength = bytesResult.Length;
                    Array.Resize<byte>(ref bytesResult, arrayLength + i);
                    Array.Copy(bytes, 0, bytesResult, arrayLength, i);
                }
                contentXml = Encoding.UTF8.GetString(bytesResult);
            }
            return Comparer.CheckTextIfMatch(s, contentXml);
        }
    }
}
