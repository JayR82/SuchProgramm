namespace WindowsFormsApplication1
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lbInitFolder = new System.Windows.Forms.TextBox();
            this.dgFoundFiles = new System.Windows.Forms.DataGridView();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.lbSuchText = new System.Windows.Forms.TextBox();
            this.btnSuche = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgFoundFiles)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbInitFolder
            // 
            this.lbInitFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInitFolder.Enabled = false;
            this.lbInitFolder.Location = new System.Drawing.Point(83, 3);
            this.lbInitFolder.Name = "lbInitFolder";
            this.lbInitFolder.Size = new System.Drawing.Size(756, 20);
            this.lbInitFolder.TabIndex = 1;
            this.lbInitFolder.Text = "Initialer Ordner zur Suche...";
            // 
            // dgFoundFiles
            // 
            this.dgFoundFiles.AllowDrop = true;
            this.dgFoundFiles.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgFoundFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFoundFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgFoundFiles.Location = new System.Drawing.Point(83, 63);
            this.dgFoundFiles.Name = "dgFoundFiles";
            this.dgFoundFiles.ReadOnly = true;
            this.dgFoundFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgFoundFiles.Size = new System.Drawing.Size(756, 276);
            this.dgFoundFiles.TabIndex = 5;
            this.dgFoundFiles.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFoundFiles_CellContentDoubleClick);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(845, 3);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(41, 23);
            this.btnOpenFolder.TabIndex = 2;
            this.btnOpenFolder.Text = "...";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbSuchText
            // 
            this.lbSuchText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSuchText.Location = new System.Drawing.Point(83, 33);
            this.lbSuchText.Name = "lbSuchText";
            this.lbSuchText.Size = new System.Drawing.Size(756, 20);
            this.lbSuchText.TabIndex = 3;
            this.lbSuchText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbSuchText_MouseClick);
            // 
            // btnSuche
            // 
            this.btnSuche.Location = new System.Drawing.Point(845, 33);
            this.btnSuche.Name = "btnSuche";
            this.btnSuche.Size = new System.Drawing.Size(75, 23);
            this.btnSuche.TabIndex = 4;
            this.btnSuche.Text = "Suche";
            this.btnSuche.UseVisualStyleBackColor = true;
            this.btnSuche.Click += new System.EventHandler(this.btnSuche_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ordner";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Suchtext";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ergebnisliste:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnOpenFolder, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSuche, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgFoundFiles, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbInitFolder, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbSuchText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(942, 342);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(842, 60);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(100, 282);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(98, 15);
            this.toolStripStatusLabel1.Text = "Dateien:";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(96, 15);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(98, 15);
            this.toolStripStatusLabel2.Text = "Treffer:";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Suche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 342);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "Suche";
            this.Text = "Suche";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFoundFiles)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox lbInitFolder;
        private System.Windows.Forms.DataGridView dgFoundFiles;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TextBox lbSuchText;
        private System.Windows.Forms.Button btnSuche;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}

