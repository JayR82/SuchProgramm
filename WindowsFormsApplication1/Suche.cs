using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
 
            SuchText = lbSuchText.Text;
            if (SuchText == "")
            {
                lbSuchText.Text = "Bitte hier ein Suchtext eingeben!!!";
            }
            else
            {
                GetAllFiles();
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
                match = FileContentStringMatch(s1[i]);
                if (match)
                {
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

        private bool FileContentStringMatch(string p)
        {
            string contents = File.ReadAllText(p);
            Regex r = new Regex(SuchText, RegexOptions.IgnoreCase);
            Match m = r.Match(contents);
            if (m.Success)
            {
                return true;
            }
            return false;
        }


        private void dgFoundFiles_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string filepath = (string)dgFoundFiles.Rows[e.RowIndex].Cells[0].Value;
            if (File.Exists(filepath))
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
