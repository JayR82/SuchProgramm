using Spire.Presentation;
using System.Text;
using System.Text.RegularExpressions;

namespace DateiSuche
{
    class FileContentStringMatchPPT
    {
        public static bool ReadFileCompateText(string path, string s)
        {
            Spire.Presentation.Presentation presentation = new Spire.Presentation.Presentation(path, FileFormat.Auto);

            Regex r = new Regex(s, RegexOptions.IgnoreCase);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < presentation.Slides.Count; i++)
            {
                for (int j = 0; j < presentation.Slides[i].Shapes.Count; j++)
                {
                    if (presentation.Slides[i].Shapes[j] is IAutoShape)
                    {
                        IAutoShape shape = presentation.Slides[i].Shapes[j] as IAutoShape;
                        if (shape.TextFrame != null)
                        {
                            foreach (TextParagraph tp in shape.TextFrame.Paragraphs)
                            {
                                Match m = r.Match(tp.Text);
                                if (m.Success)
                                {
                                    presentation.Dispose();
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            presentation.Dispose();
            return false;
        }
    }
}
