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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            folder = fbd.SelectedPath;
            lbInitFolder.Text = folder;
        }

        private void SuchText_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSuche_Click(object sender, EventArgs e)
        {
            bool result;
            string[] AllFiles;
            string[] FilteredFiles;
            ResetTable();
            result = GetSearchText();

            if (result)
            {
                AllFiles = GetAllFiles();
                GetFilteredFiles(AllFiles);
                //FillTable(FilteredFiles);
            }
        }

        private void GetFilteredFiles(string[] AllFiles)
        {
            DataRow dr;

            for (int i = 0; i <= AllFiles.Length - 1; i++)
            {
                Boolean match;
                String FileEnding;

                if (i == 0)
                {
                    //Add Data Grid Columns with name
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Typ");
                    dt.Columns.Add("Datum");
                }

                //Get each file information
                String CurrentFile = AllFiles[i];
                FileSystemInfo CurrentFileInfo = new FileInfo(CurrentFile);
                FileEnding = CurrentFileInfo.Extension;
                switch (FileEnding)
                {
                    case ".pdf":
                        {
                            match = FileContentStringMatchPDF(CurrentFile);
                            break;
                        }
                    default:
                        {
                            match = FileContentStringMatchTXT(CurrentFile);
                            break;
                        }
                }

                if (match)
                {
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
            }
            dgFoundFiles.DataSource = dt;
        }

        private bool GetSearchText()
        {
            // Get search text frim TextVarIn
            SuchText = lbSuchText.Text;
            if (SuchText == "")
            {
                lbSuchText.Text = "Bitte hier ein Suchtext eingeben!!!";
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

            string[] s1 = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

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

        private void dgFoundFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
