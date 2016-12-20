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
using Excel;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Suche : Form
    {
        string InitialDir;
        DataTable dt = new DataTable();
        string SuchText;
        int AusgelasseneDateien = 0;
        int FileReadError = 0;
        public List<string> ErrorFiles = new List<string>();
        public List<string> SkipFiles = new List<string>();

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
            toolStripStatusLabel1.Text = "Dateien: 0";
            toolStripStatusLabel2.Text = "Treffer: 0";
            toolStripStatusLabel3.Text = "Ausgelassen: 0";
            toolStripStatusLabel4.Text = "Lesefehler: 0";
            toolStripStatusLabel5.Text = "Status:\nBereit für Suche";
            AusgelasseneDateien = 0;
            FileReadError = 0;
            ErrorFiles.Clear();
            SkipFiles.Clear();

            //Reset table
            dt.Clear();
            dgFoundFiles.DataSource = dt;

            //Delete columns
            if (dt.Columns.Contains("Pfad"))
            {
                dt.Columns.Remove("Pfad");
                dt.Columns.Remove("Name");
                dt.Columns.Remove("Typ");
                dt.Columns.Remove("Erstellt Datum");
                dt.Columns.Remove("Geändert Datum");
            }
            statusStrip1.Refresh();
            dgFoundFiles.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (Directory.Exists(InitialDir))
            {
                fbd.SelectedPath = InitialDir;
            }
            
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                InitialDir = fbd.SelectedPath;
                lbInitFolder.Text = InitialDir;
            }
        }

        private void btnSuche_Click(object sender, EventArgs e)
        {
            List<string> AllFiles;
            List<string> MatchedFiles;

            btnOpenFolder.Enabled = false;
            btnSuche.Enabled = false;
            lbSuchText.Enabled = false;
            
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
                toolStripStatusLabel2.Text = string.Format("Treffer: {0}", dgFoundFiles.RowCount);
                toolStripStatusLabel3.Text = string.Format("Ausgelassen: {0}", AusgelasseneDateien);
                toolStripStatusLabel4.Text = string.Format("Lesefehler: {0}", FileReadError);
                toolStripStatusLabel5.Text = "Status:\nSuche fertig";
            }
            lbSuchText.Enabled = true;
            btnSuche.Enabled = true;
            btnOpenFolder.Enabled = true;
        }

        private bool GetSearchText()
        {
            // Get search text frim TextVarIn
            SuchText = lbSuchText.Text;
            if (SuchText == "")
            {
                toolStripStatusLabel5.Text = "Status:\nSuchText\neingeben!";
                return false;
            }

            //Replace som special characters for RegEx
            SuchText = SuchText.Replace(@"\", @"\\");
            SuchText = SuchText.Replace(@"^", @"\^");
            SuchText = SuchText.Replace(@"$", @"\$");
            SuchText = SuchText.Replace(@".", @"\.");
            SuchText = SuchText.Replace(@"|", @"\|");
            SuchText = SuchText.Replace(@"?", @"\?");
            SuchText = SuchText.Replace(@"*", @"\*");
            SuchText = SuchText.Replace(@"+", @"\+");
            SuchText = SuchText.Replace(@"(", @"\(");
            SuchText = SuchText.Replace(@")", @"\)");
            SuchText = SuchText.Replace(@"{", @"\{");
            SuchText = SuchText.Replace(@"}", @"\}");
            SuchText = SuchText.Replace(@"[", @"\[");
            SuchText = SuchText.Replace(@"]", @"\]");

            return true;
        }

        private bool GetInitialDir()
        {
            // Get initial directory for file search
            if (!Directory.Exists(InitialDir))
            {
                toolStripStatusLabel5.Text = "Status:\nInitialen Ordner\neingeben!";
                return false;
            }
            return true;
        }

        private List<string> GetAllFiles()
        {
            int FileCount;

            toolStripStatusLabel5.Text = "Status:\nSuche alle \nDateien";
            statusStrip1.Refresh();

            IEnumerable<string> s1 = GetFiles(InitialDir, "*");
            List<string> AllFiles = new List<string>(s1);
            FileCount = AllFiles.Count;

            toolStripStatusLabel1.Text = string.Format("Dateien: {0}", FileCount);

            return AllFiles;
        }

        public static IEnumerable<string> GetFiles(string root, string searchPattern)
        {
            Stack<string> pending = new Stack<string>();
            pending.Push(root);
            while (pending.Count != 0)
            {
                var path = pending.Pop();
                string[] next = null;
                try
                {
                    next = Directory.GetFiles(path, searchPattern);
                }
                catch { }
                if (next != null && next.Length != 0)
                    foreach (var file in next) yield return file;
                try
                {
                    next = Directory.GetDirectories(path);
                    foreach (var subdir in next) pending.Push(subdir);
                }
                catch { }
            }
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
            toolStripStatusLabel5.Text = "Status:\nDurchsuche alle\nDateien";

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
                        case ".LOG":
                        case ".XML":
                        case ".XAML":
                        case ".CS":
                        case ".DAT":
                        case ".CONFIG":
                        case ".BAK":
                        case ".HTM":
                        case ".HTML":
                        case ".INI":
                        case ".CSPROJ":
                        case ".SVB":
                        case ".tcKDTest":
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
                        //case ".PPT":
                        //    {
                        //        match = FileContentStringMatchPPT(CurrentFile);
                        //        break;
                        //    }
                            //case ".PPTX":
                        //    {
                        //        match = FileContentStringMatchPPTX(CurrentFile);
                         //       break;
                        //   }
                        case ".XLS":
                        case ".CSV":
                            {
                                match = FileContentStringMatchXLS(CurrentFile);
                                break;
                            }
                        case ".XLSX":
                            {
                                match = FileContentStringMatchXLSX(CurrentFile);
                                break;
                            }
                            //case ".ODS":
                        //    {
                        //        match = FileContentStringMatchODS(CurrentFile);
                        //        break;
                        //    }
                            //case ".ODT":
                        //    {
                         //       match = FileContentStringMatchODT(CurrentFile);
                         //       break;
                        //   }
                        default:
                            {
                                match = false;
                                AusgelasseneDateien++;
                                SkipFiles.Add(CurrentFile);
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
                    FileReadError++;
                    ErrorFiles.Add(CurrentFile);
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

        private bool FileContentStringMatchPPTX(string p)
        {
            return true;                                      
        }

        private bool FileContentStringMatchXLS(string p)
        {
            Workbook workbook = Workbook.getWorkbook(new System.IO.FileInfo(p));
            int sheets = workbook.getNumberOfSheets();
            Regex r = new Regex(SuchText, RegexOptions.IgnoreCase);
            for (int i = 0; i < sheets; i++)
            {
                var sheet = workbook.getSheet(i);
                CSharpJExcel.Jxl.Cell CellContent = sheet.findCell(r, 0, 0, sheet.getColumns(), sheet.getRows(), false);
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

        private bool FileContentStringMatchXLSX(string p)
        {
            FileStream stream = File.Open(p, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            Regex r = new Regex(SuchText, RegexOptions.IgnoreCase);
            DataSet result = excelReader.AsDataSet();
            
            for (int i = 0; i < result.Tables.Count; i++)
			{
                for (int j = 0; j < result.Tables[i].Rows.Count; j++)
                {
                    object[] row = result.Tables[i].Rows[j].ItemArray;
                    foreach (object item in row)
                    {
                        Match m = r.Match(item.ToString());
                        if (m.Success)
                        {
                            excelReader.Close();
                            return true;
                        }
                    }
                }
			}

            excelReader.Close();
            return false;
        }

        private bool FileContentStringMatchODS(string p)
        {
            return true;
        }

        private bool FileContentStringMatchODT(string p)
        {
            return true;  
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
                    dt.Columns.Add("Pfad");
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Typ");
                    dt.Columns.Add("Erstellt Datum");
                    dt.Columns.Add("Geändert Datum");
                }

                //Prepare table content
                dr = dt.NewRow();
                //Get File name of each file name
                dr["Pfad"] = CurrentFileInfo.FullName;
                //Get File name of each file name
                dr["Name"] = CurrentFileInfo.Name;
                //Get File Type/Extension of each file 
                dr["Typ"] = CurrentFileInfo.Extension;
                //Get file Create Date
                dr["Erstellt Datum"] = CurrentFileInfo.CreationTime.Date.ToString("dd/MM/yyyy");
                //Get file Create Date
                dr["Geändert Datum"] = CurrentFileInfo.LastWriteTime.Date.ToString("dd/MM/yyyy");
                //Insert collected file details in Datatable
                dt.Rows.Add(dr);
            }
            
            //Write to table
            dgFoundFiles.DataSource = dt;
            //Delete duplicates in DataGridView
            DeleteDuplicates(dgFoundFiles);
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
            if (e.RowIndex >= 0)
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
           
        }

        private void dgFoundFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string filepath = (string)this.dgFoundFiles.Rows[this.dgFoundFiles.CurrentCell.RowIndex].Cells[0].Value;
                if (System.IO.File.Exists(filepath))
                {
                    System.Diagnostics.Process.Start(filepath);
                }
                else
                {
                    MessageBox.Show("" + filepath + " kann nicht geöffnet werden!");
                }
            }
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Jürgen Reutter
V1.00", "Version",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@".PDF / .DOC / .DOCX / .CSV / .XLS / .XLSX
.TXT / .LOG / .DAT / .HTM / .HTML / .XML / .XAML
.CONFIG / .INI / .CSPROJ / .CS / .SVB / .tcKDTest", "Unterstützte Dateiformate",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void anleitungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Dieses Programm durchsucht textbasierte Dateien in einem angegebenen Ordner und dessen Unterordner, auf ein oder mehrere zusammenhängende Worte.

1. Gib den Initialen Ordner an, in dem Dateien durchsucht werden sollen
1.1. Click [...] und navigiere im gezeigten Dialog zu deinem initialen Ordner, bestätige mit 'OK'
1.1.1 Vermeide direkt Partitionen anzugeben (D:\ oder C:\) wegen evtl. sehr großer Datenmenge
1.1.2 Durchsuchen von Dateien auf USB-Stick = kein Problem
1.1.3 Durchsuchen von Dateien auf Server / NAS = kein Problem (kann ein bisschen länger dauern, bis die Suche fertig ist)
1.1.4 Durchsuchen von Dateien auf CD/DVD-Laufwerk = keine Ahnung -habe ich nicht getestet
1.2. Je genauer du den initialen Ordner eingrenzt, desto schneller ist die Suche fertig
1.3. Schau im Menü unter 'Hilfe-Info' nach, ob alle erwartenden Dateiformate unterstützt werden
1.3.1 Bildbasierte Dateien, sowie Container (.zip) und Verknüpfungen werden ausgelassen und nicht durchsucht

2. Gib ein Suchtext ein, den die zu suchende Datei beinhaltet
2.1. Ein einzelnes Wort oder ein Wortausschnitt
2.2. Bei einer Suchtext-Eingabe mehrerer Wörter, sollten diese auch so in dieser Reihenfolge tatsächlich vorkommen
2.3. Groß- / Kleinschreibung im Suchtext ist egal (Suchtext 'Rechnung' findet auch 'ABRECHNUNG' in Dateien)
2.4. Wildcards (*, ?, %, #) sind nicht nötig um ein Suchtext zu erweitern. Lieber nur nach einem Wortausschnitt suchen
        
3. Starte die Suche
3.1. Drück 'Suche' oder die Taste -Enter-

4. Statusmeldungen werden rechts angezeigt
4.1. 'Dateien': Zeigt die Anzahl der Dateien, die in dem initialen Ordner und dessen Unterordner insgesamt gefunden wurden
4.2. 'Statusbalken': Zeigt an, ob die Suche beendet ist (Balken is komplett grün)
4.3. 'Ausgelassen': Zeigt an, wieviele Dateien nicht durchsucht werden konnten (Bspw. wegen nicht unterstütztem Dateiformat wie Bilder oder Musik)
4.3.1 Klick auf 'Ausgelassen' oder über das Menü - Ausnahmen, um die Liste der ausgelassenen Dateien zu sehen
4.3.2 Sind es mehr als 30 Dateien die ausgelassen wurden, werden nicht alle angezeigt
4.4. 'Treffer': Zeigt die Anzahl der Dateien in denen der 'Suchtext' gefunden wurde 
4.4.1. Treffer sind: 'Suchtext' im Pfadnamen
4.4.2. Treffer sind: 'Suchtext' im Dateiinhalt
4.5. 'Lesefehler': Zeigt die Anzahl der Dateien, die potentielle Kandidaten waren, aber Fehler beim Lesen verursacht haben (keine Zugriffsrechte o.ä.) 
4.5.1 Ist der 'Lesefehler' > 0 könnte eine relevante Datei nicht in der Ergebnisliste aufgeführt sein
4.5.2 Klick auf 'Lesefehler' oder über das Menü - Ausnahmen, um die Liste der Dateien mit Lesefehlern zu sehen
4.5.3 Sind es mehr als 30 Dateien die Fehler verursacht haben, werden nicht alle angezeigt
4.6. 'Status:' Was macht das Programm gerade

5. Öffne die relevante Datei aus der Ergebnisliste
5.1. Sortiere die Spalten der Ergebnisliste nach deinen Kriterien (Name, Format, Änderungsdatum...)
5.2. Öffne eine Datei (oder mehrere Dateien nacheinander) durch Doppelklick auf die ausgewählte Zeile oder drücke die Taste -Enter- um die markierte Datei (Zeile) zu öffnen
", "Suche Anleitung", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void anzeigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Ausgabe;
            string ErrorFileList = "";
            if (ErrorFiles.Count > 30)
            {
                for (int i = 0; i < 11; i++)
                {
                    ErrorFileList += ErrorFiles[i].ToString() + "\n";
                }

                Ausgabe = String.Format(@"{0} Datei(en) bei denen es Lesefehler gab.

Es gab ziemlich viele Lesefehler, also Dateien die eigentlich durchsucht werden könnten, ergaben Lesefehler.
Bitte stelle sicher, dass die nötigen Abhängigkeiten erfüllt sind.
Siehe im 'Menu - Hilfe - Abhängigkeiten'...

Auszug der Dateien mit Lesefehler:
{1}...", ErrorFiles.Count, ErrorFileList);
                MessageBox.Show(Ausgabe, "Lesefehler bei Dateien",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ErrorFiles.Count == 0)
            {
                MessageBox.Show("Bei der letzten Suche gab es keine Lesefehler.", "Lesefehler bei Dateien",
               MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
	        {
                foreach (String s in ErrorFiles)
                {
                    ErrorFileList += s.ToString() + "\n";
                }
                Ausgabe = String.Format("{0} Datei(en) bei denen es Lesefehler gab:\n\n{1}", ErrorFiles.Count, ErrorFileList);
                MessageBox.Show(Ausgabe, "Lesefehler bei Dateien",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
	        }
            
        }

        private void ausgelassenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Ausgabe;
            string SkipFileList = "";
            if (SkipFiles.Count > 30)
            {
                for (int i = 0; i < 11; i++)
                {
                    SkipFileList += SkipFiles[i].ToString() + "\n";
                }
                
                Ausgabe = String.Format(@"{0} Dateien die nicht durchsucht wurden.

Ziemlich viele Dateien wurden bei der Suche nicht berücksichtigt.
Evtl. sind viele Bild- oder Musik-Dateien dabei??
Kontrolliere im 'Menü - Hilfe - Info' die unterstützen Dateien.

Auszug der ausgelassenen Dateien:
{1}...", SkipFiles.Count, SkipFileList);
                MessageBox.Show(Ausgabe, "Ausgelassene Dateien",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (SkipFiles.Count == 0)
            {
                MessageBox.Show("Bei der letzten Suche wurden alle Dateien berücksichtigt.", "Ausgelassene Dateien",
               MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                foreach (String s in SkipFiles)
                {
                    SkipFileList += s.ToString() + "\n";
                }
                Ausgabe = String.Format("{0} Dateien die nicht durchsucht wurden:\n\n{1}", SkipFiles.Count, SkipFileList);
                MessageBox.Show(Ausgabe, "Ausgelassene Dateien",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            ausgelassenToolStripMenuItem_Click(sender, e);
        }

        private void toolStripStatusLabel4_Click(object sender, EventArgs e)
        {
            anzeigenToolStripMenuItem_Click(sender, e);
        }
    }
}
