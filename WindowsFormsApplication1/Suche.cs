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
            //Reset table
            dt.Clear();
            dgFoundFiles.DataSource = dt;
            
            if (dt.Columns.Contains("Name"))
	        {
		        dt.Columns.Remove("Name");
                dt.Columns.Remove("Typ");
                dt.Columns.Remove("Datum");
	        }
            

            // Get search text frim TextVarIn
            SuchText = lbSuchText.Text;
            if (SuchText == "")
            {
                lbSuchText.Text = "Bitte hier ein Suchtext eingeben!!!";
            }
            else
            {
                // Get all files matching search text
                GetAllFiles();
                //Filling table
                dgFoundFiles.DataSource = dt;
            }
        }

        private void GetAllFiles()
        {
 	        DataRow dr;
           
            // Process the list of files found in the directory.
            string[] s1 = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
           
            for (int i = 0; i <= s1.Length - 1; i++)
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
                FileInfo f = new FileInfo(s1[i]);
                FileSystemInfo f1 = new FileInfo(s1[i]);
                FileEnding = f1.Extension;
                switch (FileEnding)
                {
                    case ".pdf":
                        {
                            match = FileContentStringMatchTXT(s1[i]);
                            break;
                        }
                    default:
                        {
                            match = FileContentStringMatchTXT(s1[i]);
                            break;
                        }
                }
                match = FileContentStringMatchTXT(s1[i]);
                if (match)
                {
                    dr = dt.NewRow();
                    //Get File name of each file name
                    dr["Name"] = f1.FullName;
                    //Get File Type/Extension of each file 
                    dr["Typ"] = f1.Extension;
                    //Get file Create Date and Time 
                    dr["Datum"] = f1.CreationTime.Date.ToString("dd/MM/yyyy");
                    //Insert collected file details in Datatable
                    dt.Rows.Add(dr);
                }
            }
        }

        private bool FileContentStringMatchTXT(string p)
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

        private bool FileContentStringMatchPDF(string p)
        {
            //PdfReader reader2 = new PdfReader((string)Filename);
            //string strText = string.Empty;

            //for (int page = 1; page <= reader2.NumberOfPages; page++)
            //{
            //    ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
            //    PdfReader reader = new PdfReader((string)Filename);
            //    String s = PdfTextExtractor.GetTextFromPage(reader, page, its);

            //    s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
            //    strText = strText + s;
            //    reader.Close();
            //}
            //return strText;

            return true;
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
