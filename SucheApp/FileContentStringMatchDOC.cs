using Code7248.word_reader;

namespace DateiSuche
{
    class FileContentStringMatchDOC
    {
        public static bool ReadFileCompateText(string path, string s)
        {
            TextExtractor extractor = new TextExtractor(path);
            //The string 'text' is now loaded with the text from the Word Document
            string text = extractor.ExtractText(); 
            return Comparer.CheckTextIfMatch(s, text);
        }
    }
}
