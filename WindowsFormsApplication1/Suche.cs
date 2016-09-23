using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;



namespace WindowsFormsApplication1
{
    public partial class Suche : Form
    {
        string folder;
        DataTable dt = new DataTable();
        string SuchText;

        public Suche()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

 
        private void button1_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            folder = fbd.SelectedPath;
            lbInitFolder.Text = folder;
        }


        private void btnSuche_Click(object sender, EventArgs e)
        {
            bool execute;
            string[] AllFiles;
           
            ResetTable();
            execute = GetSearchText();

            if (execute)
            {
                AllFiles = GetAllFiles();
                List<string> MatchedFiles = new List<string>(AllFiles);
                MatchedFiles = GetMatchedFiles(AllFiles);
                if (MatchedFiles.Count > 0)
                {
                    FillTable(MatchedFiles);
                }
                
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
        }

        private List<string> GetMatchedFiles(string[] AllFiles)
        {
            
            List<string> MatchedFiles = new List<string>();
            int j = 0;
            int FileCount;

            toolStripProgressBar1.Maximum = AllFiles.Length;

            for (int i = 0; i <= AllFiles.Length - 1; i++)
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
                        {
                            match = FileContentStringMatchTXT(CurrentFile);
                            match = false;
                            break;
                        }
                    case ".pdf":
                        {
                            match = FileContentStringMatchPDF(CurrentFile);
                            break;
                        }
                    case ".doc": case ".docx":
                        {
                            //match = FileContentStringMatchPDF(CurrentFile);
                            match = false;
                            break;
                        }
                    case ".ppt": case ".pptx":
                        {
                            //match = FileContentStringMatchPDF(CurrentFile);
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
                    MatchedFiles[j] = CurrentFile;
                    j++;
                }

            }
            FileCount = MatchedFiles.Count;
            toolStripStatusLabel2.Text = string.Format("Treffer: {0}", FileCount);

            return MatchedFiles;
            
        }

        private bool GetSearchText()
        {
            // Get search text frim TextVarIn
            SuchText = lbSuchText.Text;
            if (SuchText == "")
            {
                
                toolStripStatusLabel1.Text = "SuchText \n eingeben!";
                statusStrip1.Refresh();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ResetTable()
        {
            //Reset table
            dt.Clear();
            dgFoundFiles.DataSource = dt;

            if (dt.Columns.Contains("Name"))
            {
                dt.Columns.Remove("Name");
                dt.Columns.Remove("Typ");
                dt.Columns.Remove("Datum");
            }
        }

        private string[] GetAllFiles()
        {
            int FileCount;
            string[] s1 = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
            FileCount = s1.Length;
            toolStripStatusLabel1.Text = string.Format("Dateien: {0}", FileCount);
            statusStrip1.Refresh();

            return s1;
        }

        private bool FileContentStringMatchTXT(string p)
        {
            try
            {
                string contents = System.IO.File.ReadAllText(p);
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

        private bool FileContentStringMatchPDF(string p)
        {
            //try
            //{
            //    // create an instance of the pdfparser class
            //    PDFParser pdfParser = new PDFParser();

            //    // extract the text
            //    bool result = pdfParser.ExtractText(p, "temp.txt");

            //    return result;
            //}
            //catch (Exception)
            //{

                return false;
            //}

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

        private void lbSuchText_MouseClick(object sender, MouseEventArgs e)
        {
            lbSuchText.Text = "";
        }

        

    }
}
