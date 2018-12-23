namespace SucheApp
{
    partial class Suche
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Suche));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lbInitFolder = new System.Windows.Forms.TextBox();
            this.dgFilesFound = new System.Windows.Forms.DataGridView();
            this.btnOpenBrowseFolder = new System.Windows.Forms.Button();
            this.lbEditSearchText = new System.Windows.Forms.TextBox();
            this.btnStartSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripMatches = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSkipped = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripReadError = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anleitungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lesefehlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anzeigenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ausgelassenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgFilesFound)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbInitFolder
            // 
            this.lbInitFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInitFolder.Enabled = false;
            this.lbInitFolder.Location = new System.Drawing.Point(83, 3);
            this.lbInitFolder.Name = "lbInitFolder";
            this.lbInitFolder.Size = new System.Drawing.Size(604, 20);
            this.lbInitFolder.TabIndex = 1;
            this.lbInitFolder.TabStop = false;
            this.lbInitFolder.Text = "Initialen Ordner zur Suche eingeben...";
            // 
            // dgFilesFound
            // 
            this.dgFilesFound.AllowUserToAddRows = false;
            this.dgFilesFound.AllowUserToResizeRows = false;
            this.dgFilesFound.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgFilesFound.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgFilesFound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFilesFound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFilesFound.Location = new System.Drawing.Point(83, 63);
            this.dgFilesFound.MultiSelect = false;
            this.dgFilesFound.Name = "dgFilesFound";
            this.dgFilesFound.ReadOnly = true;
            this.dgFilesFound.RowHeadersVisible = false;
            this.dgFilesFound.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgFilesFound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFilesFound.Size = new System.Drawing.Size(604, 304);
            this.dgFilesFound.TabIndex = 4;
            this.dgFilesFound.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgFoundFiles_CellDoubleClick);
            this.dgFilesFound.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgFoundFiles_KeyDown);
            // 
            // btnOpenBrowseFolder
            // 
            this.btnOpenBrowseFolder.Location = new System.Drawing.Point(693, 3);
            this.btnOpenBrowseFolder.Name = "btnOpenBrowseFolder";
            this.btnOpenBrowseFolder.Size = new System.Drawing.Size(38, 20);
            this.btnOpenBrowseFolder.TabIndex = 1;
            this.btnOpenBrowseFolder.Text = "...";
            this.btnOpenBrowseFolder.UseVisualStyleBackColor = true;
            this.btnOpenBrowseFolder.Click += new System.EventHandler(this.Button1_Click);
            // 
            // lbEditSearchText
            // 
            this.lbEditSearchText.AcceptsReturn = true;
            this.lbEditSearchText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEditSearchText.Location = new System.Drawing.Point(83, 33);
            this.lbEditSearchText.Name = "lbEditSearchText";
            this.lbEditSearchText.Size = new System.Drawing.Size(604, 20);
            this.lbEditSearchText.TabIndex = 2;
            this.lbEditSearchText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbEditSearchText_MouseClick);
            this.lbEditSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbEditSearchText_KeyDown);
            // 
            // btnStartSearch
            // 
            this.btnStartSearch.Location = new System.Drawing.Point(693, 33);
            this.btnStartSearch.Name = "btnStartSearch";
            this.btnStartSearch.Size = new System.Drawing.Size(65, 20);
            this.btnStartSearch.TabIndex = 3;
            this.btnStartSearch.Text = "Suche";
            this.btnStartSearch.UseVisualStyleBackColor = true;
            this.btnStartSearch.Click += new System.EventHandler(this.btnStartSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 30);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ordner";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Suchtext";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ergebnisliste";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnOpenBrowseFolder, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnStartSearch, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgFilesFound, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbInitFolder, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbEditSearchText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(790, 370);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar,
            this.toolStripMatches,
            this.toolStripSkipped,
            this.toolStripReadError,
            this.toolStripStatusLabel6,
            this.toolStripStatus});
            this.statusStrip.Location = new System.Drawing.Point(690, 60);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(100, 310);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(98, 15);
            this.toolStripStatusLabel1.Text = "Dateien:";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(96, 15);
            // 
            // toolStripMatches
            // 
            this.toolStripMatches.Name = "toolStripMatches";
            this.toolStripMatches.Size = new System.Drawing.Size(98, 15);
            this.toolStripMatches.Text = "Treffer:";
            this.toolStripMatches.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSkipped
            // 
            this.toolStripSkipped.Name = "toolStripSkipped";
            this.toolStripSkipped.Size = new System.Drawing.Size(98, 15);
            this.toolStripSkipped.Text = "Ausgelassen:";
            this.toolStripSkipped.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripSkipped.Click += new System.EventHandler(this.toolStripSkipped_Click);
            // 
            // toolStripReadError
            // 
            this.toolStripReadError.Name = "toolStripReadError";
            this.toolStripReadError.Size = new System.Drawing.Size(98, 15);
            this.toolStripReadError.Text = "Lesefehler:";
            this.toolStripReadError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripReadError.Click += new System.EventHandler(this.toolStripReadError_Click);
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(98, 0);
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(98, 15);
            this.toolStripStatus.Text = "Status: Bereit";
            this.toolStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hilfeToolStripMenuItem,
            this.lesefehlerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(790, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anleitungToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.versionToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // anleitungToolStripMenuItem
            // 
            this.anleitungToolStripMenuItem.Name = "anleitungToolStripMenuItem";
            this.anleitungToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.anleitungToolStripMenuItem.Text = "Anleitung";
            this.anleitungToolStripMenuItem.Click += new System.EventHandler(this.AnleitungToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.InfoToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.versionToolStripMenuItem.Text = "Version";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.VersionToolStripMenuItem_Click);
            // 
            // lesefehlerToolStripMenuItem
            // 
            this.lesefehlerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anzeigenToolStripMenuItem,
            this.ausgelassenToolStripMenuItem});
            this.lesefehlerToolStripMenuItem.Name = "lesefehlerToolStripMenuItem";
            this.lesefehlerToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.lesefehlerToolStripMenuItem.Text = "Ausnahmen";
            // 
            // anzeigenToolStripMenuItem
            // 
            this.anzeigenToolStripMenuItem.Name = "anzeigenToolStripMenuItem";
            this.anzeigenToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.anzeigenToolStripMenuItem.Text = "Lesefehler zeigen";
            this.anzeigenToolStripMenuItem.Click += new System.EventHandler(this.AnzeigenToolStripMenuItem_Click);
            // 
            // ausgelassenToolStripMenuItem
            // 
            this.ausgelassenToolStripMenuItem.Name = "ausgelassenToolStripMenuItem";
            this.ausgelassenToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.ausgelassenToolStripMenuItem.Text = "Ausgelassen zeigen";
            this.ausgelassenToolStripMenuItem.Click += new System.EventHandler(this.AusgelassenToolStripMenuItem_Click);
            // 
            // Suche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 394);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "Suche";
            this.Text = "Suche";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFilesFound)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox lbInitFolder;
        private System.Windows.Forms.DataGridView dgFilesFound;
        private System.Windows.Forms.Button btnOpenBrowseFolder;
        private System.Windows.Forms.TextBox lbEditSearchText;
        private System.Windows.Forms.Button btnStartSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripMatches;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anleitungToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSkipped;
        private System.Windows.Forms.ToolStripStatusLabel toolStripReadError;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripMenuItem lesefehlerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anzeigenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ausgelassenToolStripMenuItem;
    }
}

