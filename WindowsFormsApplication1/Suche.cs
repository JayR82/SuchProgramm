using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using Code7248.word_reader;

namespace WindowsFormsApplication1
{
    public partial class Suche : Form
    {
        string InitialDir;
        DataTable dt = new DataTable();
        string SuchText;
        int MatchCount;

        public Suche()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void lbSuchText_MouseClick(object sender, MouseEventArgs e)
        {
            lbSuchText.Text = "";
        }

        private void lbSuchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSuche_Click((object)sender, (EventArgs)e);
            }
        }

        private void Reset()
        {
            //Reset status
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel2.Text = "Treffer: 0";
            MatchCount = 0;

            //Reset table
            dt.Clear();
            dgFoundFiles.DataSource = dt;

            //Delete columns
            if (dt.Columns.Contains("Name"))
            {
                dt.Columns.Remove("Name");
                dt.Columns.Remove("Typ");
                dt.Columns.Remove("Datum");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            InitialDir = fbd.SelectedPath;
            lbInitFolder.Text = InitialDir;
        }

        private void btnSuche_Click(object sender, EventArgs e)
        {
            List<string> AllFiles;
            List<string> MatchedFiles;

            Reset();
           
            if (GetInitialDir() && GetSearchText())
            {
                AllFiles = GetAllFiles();
                //Find search text in file URL
                MatchedFiles = GetMatchedURL(AllFiles);
                //Find search text in file content
                MatchedFiles.AddRange(GetMatchedFiles(AllFiles));
                if (MatchedFiles.Count > 0)
                {
                    FillTable(MatchedFiles);
                }
            }
        }


        private bool GetSearchText()
        {
            // Get search text frim TextVarIn
            SuchText = lbSuchText.Text;
            if (SuchText == "")
            {
                toolStripStatusLabel1.Text = "SuchText\neingeben!";
                return false;
            }
            
            return true;
        }

        private bool GetInitialDir()
        {
            // Get initial directory for file search
            if (!Directory.Exists(InitialDir))
            {
                toolStripStatusLabel1.Text = "Initialen Ordner\neingeben!";
                return false;
            }
            return true;
        }

        private List<string> GetAllFiles()
        {
            int FileCount;

            string[] s1 = Directory.GetFiles(InitialDir, "*", SearchOption.AllDirectories);

            FileCount = s1.Length;
            toolStripStatusLabel1.Text = string.Format("Dateien: {0}", FileCount);
            statusStrip1.Refresh();

            //Set string array ti list
            List<string> AllFiles = new List<string>(s1);

            return AllFiles;
        }

        private List<string> GetMatchedURL(List<string> AllFiles)
        {
            List<string> MatchedURLs = new List<string>();

            for (int i = 0; i <= AllFiles.Count - 1; i++)
            {
                String CurrentFileURL = AllFiles[i];

                Regex r = new Regex(SuchText, RegexOptions.IgnoreCase);
                Match m = r.Match(CurrentFileURL);
                if (m.Success)
                {
                    MatchedURLs.Add(CurrentFileURL);
                }
            }

            MatchCount += MatchedURLs.Count;
            toolStripStatusLabel2.Text = string.Format("Treffer: {0}", MatchCount);

            return MatchedURLs;
        }

        private List<string> GetMatchedFiles(List<string> AllFiles)
        {

            List<string> MatchedFiles = new List<string>();      

            toolStripProgressBar1.Maximum = AllFiles.Count;

            for (int i = 0; i <= AllFiles.Count - 1; i++)
            {
                Boolean match;
                String FileEnding;
                String CurrentFile = AllFiles[i];

                toolStripProgressBar1.Value += 1;
                FileSystemInfo CurrentFileInfo = new FileInfo(CurrentFile);
                FileEnding = CurrentFileInfo.Extension;
                switch (FileEnding)
                {
                    case ".txt":
                    case ".xml":
                    case ".xaml":
                    case ".cs":
                        {
                            match = FileContentStringMatchTXT(CurrentFile);
                            break;
                        }
                    case ".pdf":
                        {
                            match = FileContentStringMatchPDF(CurrentFile);
                            break;
                        }
                    case ".doc":
                    case ".docx":
                        {
                            match = FileContentStringMatchDOC(CurrentFile);
                            break;
                        }
                    case ".ppt":
                    case ".pptx":
                        {
                            //match = FileContentStringMatchPDF(CurrentFile);
                            match = false;
                            break;
                        }
                    case ".xls":
                        {
                            //match = FileContentStringMatchXLS(CurrentFile);
                            match = false;
                            break;
                        }
                    default:
                        {
                            match = false;
                            break;
                        }
                }

                if (match)
                {
                    MatchedFiles.Add(CurrentFile);
                }

            }

            MatchCount += MatchedFiles.Count;
            toolStripStatusLabel2.Text = string.Format("Treffer: {0}", MatchCount);
            
            return MatchedFiles;
        }

        private bool FileContentStringMatchTXT(string p)
        {
            string contents = System.IO.File.ReadAllText(p);
            return FindText(contents);
        }

        private bool FileContentStringMatchPDF(string p)
        {
             using (PdfReader reader = new PdfReader(p))
            {
                StringBuilder text = new StringBuilder();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
               
                return FindText(text.ToString());
            }
        }

  

        private bool FileContentStringMatchDOC(string p)
        {
            TextExtractor extractor = new TextExtractor(p);
            string text = extractor.ExtractText(); //The string 'text' is now loaded with the text from the Word Document
            return FindText(text);
        }

        private bool FileContentStringMatchPPT(string p)
        {
            return false;
        }

        private bool FileContentStringMatchXLS(string p)
        {
            return false;
        }

        private void FillTable(List<string> MatchedFiles)
        {
            DataRow dr;

            for (int i = 0; i <= MatchedFiles.Count - 1; i++)
            {
                FileSystemInfo CurrentFileInfo = new FileInfo(MatchedFiles[i]);

                if (i == 0)
                {
                    //Add Data Grid Columns with name
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Typ");
                    dt.Columns.Add("Datum");
                }

                //Prepare table content
                dr = dt.NewRow();
                //Get File name of each file name
                dr["Name"] = CurrentFileInfo.FullName;
                //Get File Type/Extension of each file 
                dr["Typ"] = CurrentFileInfo.Extension;
                //Get file Create Date and Time 
                dr["Datum"] = CurrentFileInfo.CreationTime.Date.ToString("dd/MM/yyyy");
                //Insert collected file details in Datatable
                dt.Rows.Add(dr);
            }
            //Write to table
            dgFoundFiles.DataSource = dt;
        }

        private void dgFoundFiles_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string filepath = (string)dgFoundFiles.Rows[e.RowIndex].Cells[0].Value;
            if (System.IO.File.Exists(filepath))
            {
                System.Diagnostics.Process.Start(filepath);
            }
            else
            {
                MessageBox.Show("" + filepath + " kann nicht geöffnet werden!");
            }

        }

       
        private bool FindText(string contents)
        {
            try
            {
                Regex r = new Regex(SuchText, RegexOptions.IgnoreCase);
                Match m = r.Match(contents);
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
