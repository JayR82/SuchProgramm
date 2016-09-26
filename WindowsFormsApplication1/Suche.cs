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
using CSharpJExcel.Jxl;


namespace WindowsFormsApplication1
{
    public partial class Suche : Form
    {
        string InitialDir;
        DataTable dt = new DataTable();
        string SuchText;
       

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

                try
                {
                    toolStripProgressBar1.Value += 1;

                    FileSystemInfo CurrentFileInfo = new FileInfo(CurrentFile);
                    FileEnding = CurrentFileInfo.Extension;
                    switch (FileEnding.ToUpper())
                    {
                        case ".TXT":
                        case ".XML":
                        case ".XAML":
                        case ".CS":
                        case ".DAT":
                        case ".CONFIG":
                        case ".BAK":
                        case ".HTM":
                        case ".HTML":
                            {
                                match = FileContentStringMatchTXT(CurrentFile);
                                break;
                            }
                        case ".PDF":
                            {
                                match = FileContentStringMatchPDF(CurrentFile);
                                break;
                            }
                        case ".DOC":
                        case ".DOCX":
                            {
                                match = FileContentStringMatchDOC(CurrentFile);
                                break;
                            }
                        case ".PPT":
                        case ".PPTX":
                            {
                                match = FileContentStringMatchPPT(CurrentFile);
                                break;
                            }
                        case ".XLS":
                        case ".XLSX":
                        case ".CSV":
                            {
                                match = FileContentStringMatchXLS(CurrentFile);
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
                catch (Exception)
                {
                    MessageBox.Show("Fehler während suche!... \nIst vielleicht eine Datei aus dem Suchordner gerade offen???\n Bitte alle eventuell betroffenen Dateien schließen und suche neu starten!", "Suche",
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            
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
            Workbook workbook = Workbook.getWorkbook(new System.IO.FileInfo(p));
            int sheets = workbook.getNumberOfSheets();
            Regex r = new Regex(SuchText, RegexOptions.IgnoreCase);
            for (int i = 0; i < sheets; i++)
            {
                var sheet = workbook.getSheet(i);
                Cell CellContent = sheet.findCell(r, 0, 0, sheet.getColumns(), sheet.getRows(), false);
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
            //Delete duplicates in DataGridView
            DeleteDuplicates(dgFoundFiles);
            
            toolStripStatusLabel2.Text = string.Format("Treffer: {0}", dgFoundFiles.RowCount);
        }

        private void DeleteDuplicates(DataGridView dt)
        {
            for (int currentRow = 0; currentRow < dt.Rows.Count - 1; currentRow++)
            {
                DataGridViewRow rowToCompare = dt.Rows[currentRow];

                for (int otherRow = currentRow + 1; otherRow < dt.Rows.Count; otherRow++)
                {
                    DataGridViewRow row = dt.Rows[otherRow];
                    bool duplicateRow = true;

                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        if (!rowToCompare.Cells[cellIndex].Value.Equals(row.Cells[cellIndex].Value))
                        {
                            duplicateRow = false;
                            break;
                        }
                    }

                    if (duplicateRow)
                    {
                        dt.Rows.Remove(row);
                        otherRow--;
                    }
                }
            }
        }

        private void dgFoundFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Open file from DataGrid
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

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jürgen Reutter\nV1.00", "Suche",
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
  
    }
}
