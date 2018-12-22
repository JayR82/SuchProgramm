using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using A = DocumentFormat.OpenXml.Drawing;

namespace DateiSuche
{
    class FileContentStringMatchPPTX
    {
        public static bool ReadFileCompateText(string path, string s)
        {
            using (PresentationDocument presentationDocument = PresentationDocument.Open(path, false))
            {
                Regex r = new Regex(s, RegexOptions.IgnoreCase);

                // Get the relationship ID of the first slide.
                PresentationPart part = presentationDocument.PresentationPart;
                OpenXmlElementList slideIds = part.Presentation.SlideIdList.ChildElements;
                for (int i = 0; i < slideIds.Count; i++)
                {
                    string relId = (slideIds[i] as SlideId).RelationshipId;
                    // Get the slide part from the relationship ID.
                    SlidePart slide = (SlidePart)part.GetPartById(relId);

                    // Get the inner text of the slide:
                    IEnumerable<A.Text> texts = slide.Slide.Descendants<A.Text>();
                    foreach (A.Text text in texts)
                    {
                        Match m = r.Match(text.InnerText);
                        if (m.Success)
                        {
                            presentationDocument.Close();
                            return true;
                        }
                    }
                }
                presentationDocument.Close();
            }
            return false;
        }
    }
}
