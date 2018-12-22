using DateiSuche;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace SucheApp
{
    public partial class Suche : Form
    {
        private DataTable dtDataTable = new DataTable();
        private int SkippedFiles = 0;
        private int FileReadError = 0;
        private List<string> ErrorFiles = new List<string>();
        private List<string> SkipFiles = new List<string>();
        private List<string> AllFiles;
        private bool newFileSearch = true;
        private string _TextToSearch;

        public string InitialDir { get; set; }

        public Suche()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void LbSuchText_MouseClick(object sender, MouseEventArgs e)
        {
            lbSuchText.Text = "";
        }

        private void LbSuchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSuche_Click((object)sender, (EventArgs)e);
            }
        }

        private void Reset()
        {
            //Reset status
            toolStripProgressBar1.Value = 0;
            if (newFileSearch)
            {
                toolStripStatusLabel1.Text = "Dateien: 0";
            }

            toolStripStatusLabel2.Text = "Treffer: 0";
            toolStripStatusLabel3.Text = "Ausgelassen: 0";
            toolStripStatusLabel4.Text = "Lesefehler: 0";
            toolStripStatusLabel5.Text = "Status:\nBereit für Suche";

            SkippedFiles = 0;
            FileReadError = 0;
            ErrorFiles.Clear();
            SkipFiles.Clear();

            //Reset table
            dtDataTable.Clear();
            dgFoundFiles.DataSource = dtDataTable;

            //Delete columns
            if (dtDataTable.Columns.Contains("Pfad"))
            {
                dtDataTable.Columns.Remove("Pfad");
                dtDataTable.Columns.Remove("Name");
                dtDataTable.Columns.Remove("Typ");
                dtDataTable.Columns.Remove("Erstellt Datum");
                dtDataTable.Columns.Remove("Geändert Datum");
            }
            statusStrip1.Refresh();
            dgFoundFiles.Refresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            newFileSearch = true;
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

        private void BtnSuche_Click(object sender, EventArgs e)
        {
            List<string> MatchedFiles;

            btnOpenFolder.Enabled = false;
            btnSuche.Enabled = false;
            lbSuchText.Enabled = false;

            Reset();

            _TextToSearch = lbSuchText.Text;
            if (InitialDirExists() && (_TextToSearch != ""))
            {
                if (newFileSearch)
                {
                    AllFiles = ProvideFileList();
                    newFileSearch = false;
                }
                //Find search text in file content
                MatchedFiles = GetMatchedFiles();
                if (MatchedFiles.Count > 0)
                {
                    FillTable(MatchedFiles);
                }
                toolStripStatusLabel2.Text = string.Format("Treffer: {0}", dgFoundFiles.RowCount);
                toolStripStatusLabel3.Text = string.Format("Ausgelassen: {0}", SkippedFiles);
                toolStripStatusLabel4.Text = string.Format("Lesefehler: {0}", FileReadError);
                toolStripStatusLabel5.Text = "Status:\nSuche fertig";
            }
            else
            {
                toolStripStatusLabel5.Text = "Status:\nSuchText\neingeben!";
            }
            lbSuchText.Enabled = true;
            btnSuche.Enabled = true;
            btnOpenFolder.Enabled = true;
        }

        private bool InitialDirExists()
        {
            // Get initial directory for file search
            if (!Directory.Exists(InitialDir))
            {
                toolStripStatusLabel5.Text = "Status:\nInitialen Ordner\neingeben!";
                return false;
            }
            return true;
        }

        private List<string> ProvideFileList()
        {
            int FileCount;

            toolStripStatusLabel5.Text = "Status:\nSuche alle \nDateien";
            statusStrip1.Refresh();

            IEnumerable<string> s1 = GetAllFiles(InitialDir, "*");
            List<string> AllFiles = new List<string>(s1);
            FileCount = AllFiles.Count;

            toolStripStatusLabel1.Text = string.Format("Dateien: {0}", FileCount);

            return AllFiles;
        }

        public static IEnumerable<string> GetAllFiles(string root, string searchPattern)
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

        private List<string> GetMatchedFiles()
        {
            List<string> MatchedFiles = new List<string>();
            Boolean match;
            String FileEnding;
            String CurrentFile;
            Regex r ;
            Match m;

            toolStripProgressBar1.Maximum = AllFiles.Count;
            toolStripStatusLabel5.Text = "Status:\nDurchsuche alle\nDateien";
            statusStrip1.Refresh();

            for (int i = 0; i <= AllFiles.Count - 1; i++)
            {
                toolStripProgressBar1.Value++;
                CurrentFile = AllFiles[i];
                match = false;

                //if searched text is in file path/name do not read file
                r = new Regex(_TextToSearch, RegexOptions.IgnoreCase);
                m = r.Match(CurrentFile);
                if (m.Success)
                {
                    match = true;
                }
                else
                {
                    try
                    {

                        FileSystemInfo CurrentFileInfo = new FileInfo(CurrentFile);
                        FileEnding = CurrentFileInfo.Extension;
                        switch (FileEnding.ToUpper())
                        {
                            case ".TXT":
                            case ".LOG":
                            case ".XML":
                            case ".XAML":
                            case ".CS":
                            case ".CSV":
                            case ".DAT":
                            case ".CONFIG":
                            case ".BAK":
                            case ".HTM":
                            case ".HTML":
                            case ".INI":
                            case ".CSPROJ":
                            case ".SVB":
                            case ".TCKDTEST":
                            case ".SLN":
                            case ".TSPROJ":
                            case ".TMC":
                            case ".TPY":
                            case ".TCGTLO":
                            case ".TCPOU":
                            case ".TCDUT":
                            case ".TCTTO":
                            case ".PLCPROJ":
                            case ".VWSETTINGS":
                            case ".SETTINGS":
                            case ".RESX":
                            case ".CMD":
                            case ".USER":
                            case ".TMSETTINGS":
                                {
                                    match = FileContentStringMatchTXT.ReadFileCompateText(CurrentFile, _TextToSearch);
                                    break;
                                }
                            case ".PDF":
                                {
                                    match = FileContentStringMatchPDF.ReadFileCompateText(CurrentFile, _TextToSearch);
                                    break;
                                }
                            case ".DOC":
                            case ".DOCX":
                                {
                                    match = FileContentStringMatchDOC.ReadFileCompateText(CurrentFile, _TextToSearch);
                                    break;
                                }
                            case ".PPT":
                                {
                                    match = FileContentStringMatchPPT.ReadFileCompateText(CurrentFile, _TextToSearch);
                                    break;
                                }
                            case ".PPTX":
                                {
                                    match = FileContentStringMatchPPTX.ReadFileCompateText(CurrentFile, _TextToSearch);
                                    break;
                                }
                            case ".XLS":
                                {
                                    match = FileContentStringMatchXLS.ReadFileCompateText(CurrentFile, _TextToSearch);
                                    break;
                                }
                            case ".XLSX":
                                {
                                    match = FileContentStringMatchXLSX.ReadFileCompateText(CurrentFile, _TextToSearch);
                                    break;
                                }
                            case ".ODS":
                            case ".ODT":
                            case ".ODP":
                            case ".ODF":
                            case ".ODG":
                                {
                                    match = FileContentStringMatchODF.ReadFileCompateText(CurrentFile, _TextToSearch);
                                    break;
                                }
                            default:
                                {
                                    match = false;
                                    SkippedFiles++;
                                    SkipFiles.Add(CurrentFile);
                                    break;
                                }
                        }

                    }
                    catch (Exception)
                    {
                        FileReadError++;
                        ErrorFiles.Add(CurrentFile);
                        match = false;
                    }
                }

                if (match)
                {
                    MatchedFiles.Add(CurrentFile);
                }
            }

            //Return  list of files with matched text in its content
            return MatchedFiles;
        }       

       private string PrepareForeRegEx(string s)
        {
            //Replace som special characters for RegEx
            s = s.Replace(@"\", @"\\");
            s = s.Replace(@"$", @"\$");
            s = s.Replace(@".", @"\.");
            s = s.Replace(@"|", @"\|");
            s = s.Replace(@"^", @"\^");
            s = s.Replace(@"?", @"\?");
            s = s.Replace(@"*", @"\*");
            s = s.Replace(@"+", @"\+");
            s = s.Replace(@"(", @"\(");
            s = s.Replace(@")", @"\)");
            s = s.Replace(@"{", @"\{");
            s = s.Replace(@"}", @"\}");
            s = s.Replace(@"[", @"\[");
            s = s.Replace(@"]", @"\]");

            return s;
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
                    dtDataTable.Columns.Add("Pfad");
                    dtDataTable.Columns.Add("Name");
                    dtDataTable.Columns.Add("Typ");
                    dtDataTable.Columns.Add("Erstellt Datum");
                    dtDataTable.Columns.Add("Geändert Datum");
                }

                //Prepare table content
                dr = dtDataTable.NewRow();
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
                dtDataTable.Rows.Add(dr);
            }
            
            //Write to table
            dgFoundFiles.DataSource = dtDataTable;
        }

        private void DgFoundFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void DgFoundFiles_KeyDown(object sender, KeyEventArgs e)
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

        private void VersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jürgen Reutter\n" + typeof(Suche).Assembly.GetName().Version, "Version"
            ,MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Dateiformate die durchsucht werden können:

.PDF / .DOC / .DOCX / .CSV / .XLS / .XLSX / .PPT / .PPTX
.TXT / .LOG / .DAT / .HTM / .HTML
.ODT / .ODP / .ODS / .ODF / .ODG / .XML / .XAML

.CONFIG / .INI / .SLN / .CS / .SVB / .TCKDTEST
.CSPROJ / .TSPROJ / .PLCPROJ 
.VWSETTINGS / .SETTINGS / .TMSETTINGS 
.TcDUT / .TcTTO  / .TcGTLO  / .TcPOU / .TMC / .TPY 
.CMD / .USER / .RESX


Andere gefundene Dateiformate werden bei der Suche ausgelassen.", "Unterstützte Dateiformate",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void AnleitungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Dieses Programm durchsucht textbasierte Dateien in einem angegebenen Ordner und dessen Unterordner, auf ein oder mehrere Worte und listet die Dateien mit Treffer auf.

1. Gib den initialen Ordner an, in dem Dateien durchsucht werden sollen
1.1. Klick [...] und navigiere im gezeigten Dialog zu deinem initialen Ordner, bestätige mit 'OK'
1.1.1 Vermeide direkt Partitionen anzugeben (D:\ oder C:\) wegen evtl. sehr großer Datenmenge
1.1.2 Durchsuchen von Dateien auf USB-Stick = kein Problem
1.1.3 Durchsuchen von Dateien auf Server / NAS = kein Problem (kann ein bisschen länger dauern, bis die Suche fertig ist)
1.1.4 Durchsuchen von Dateien auf CD/DVD-Laufwerk = keine Ahnung -habe ich nicht getestet
1.2. Je genauer du den initialen Ordner eingrenzt, desto schneller ist die Suche fertig
1.3. Schau im Menü unter 'Hilfe-Info' nach, ob alle erwartenden Dateiformate unterstützt werden
1.3.1 Bild- und Tonbasierte Dateien, sowie Container (.zip) und Verknüpfungen werden ausgelassen und nicht durchsucht

2. Gib ein Suchtext ein, den die zu suchende Datei beinhaltet
2.1. Ein einzelnes Wort oder ein Wortausschnitt
2.2. Bei einer Suchtext- Eingabe mehrerer Wörter, sollten diese auch so in dieser Reihenfolge tatsächlich vorkommen
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
4.5.1 Ist der 'Lesefehler' > 0 könnte eine relevante Datei mit Treffern nicht in der Ergebnisliste aufgeführt sein
4.5.2 Klick auf 'Lesefehler' oder über das Menü - Ausnahmen, um die Liste der Dateien mit Lesefehlern zu sehen
4.5.3 Sind es mehr als 30 Dateien die Fehler verursacht haben, werden nicht alle angezeigt
4.6. 'Status:' Was macht das Programm gerade

5. Öffne die relevante Datei aus der Ergebnisliste
5.1. Sortiere die Spalten der Ergebnisliste nach deinen Kriterien (Name, Format, Änderungsdatum...)
5.2. Öffne eine Datei (oder mehrere Dateien nacheinander) durch Doppelklick auf die ausgewählte Zeile oder drücke die Taste -Enter- um die markierte Datei (Zeile) zu öffnen
", "Suche Anleitung", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void AnzeigenToolStripMenuItem_Click(object sender, EventArgs e)
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

Es gab ziemlich viele Lesefehler, also Dateien die eigentlich durchsucht werden sollten, konnten nicht gelesen werden.
U.U. keine Zugriffsrechte... Datei ist gerade geöffnet...

Auszug der Dateien mit Lesefehler:
{1}...", ErrorFiles.Count, ErrorFileList);
                MessageBox.Show(Ausgabe, "Lesefehler bei Suche",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ErrorFiles.Count == 0)
            {
                MessageBox.Show("Bei der letzten Suche gab es keine Lesefehler.", "Lesefehler bei Suche",
               MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
	        {
                foreach (String s in ErrorFiles)
                {
                    ErrorFileList += s.ToString() + "\n";
                }
                Ausgabe = String.Format("{0} Datei(en) bei denen es Lesefehler gab:\n\n{1}", ErrorFiles.Count, ErrorFileList);
                MessageBox.Show(Ausgabe, "Lesefehler bei Suche",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
	        }
            
        }

        private void AusgelassenToolStripMenuItem_Click(object sender, EventArgs e)
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
Evtl. sind viele Bild- oder Musik-Dateien dabei?
Kontrolliere im 'Menü - Hilfe - Info' die unterstützen Dateien.

Auszug der ausgelassenen Dateien:
{1}...", SkipFiles.Count, SkipFileList);
                MessageBox.Show(Ausgabe, "Ausgelassene Dateien",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (SkipFiles.Count == 0)
            {
                MessageBox.Show("Bei der letzten Suche wurden alle gefundenen Dateien durchsucht.", "Ausgelassene Dateien",
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

        private void ToolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            AusgelassenToolStripMenuItem_Click(sender, e);
        }

        private void ToolStripStatusLabel4_Click(object sender, EventArgs e)
        {
            AnzeigenToolStripMenuItem_Click(sender, e);
        }
    }
}
