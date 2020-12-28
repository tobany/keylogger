namespace keyloggerviewer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbHost = new System.Windows.Forms.ComboBox();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.tbRegex = new System.Windows.Forms.TextBox();
            this.bValidate = new System.Windows.Forms.Button();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblLineBefore = new System.Windows.Forms.Label();
            this.lblLineAfter = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblMinLen = new System.Windows.Forms.Label();
            this.lblMaxLen = new System.Windows.Forms.Label();
            this.lblRegex = new System.Windows.Forms.Label();
            this.nudBefore = new System.Windows.Forms.NumericUpDown();
            this.nudMaxLen = new System.Windows.Forms.NumericUpDown();
            this.nudMinLen = new System.Windows.Forms.NumericUpDown();
            this.nudAfter = new System.Windows.Forms.NumericUpDown();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.cbTime = new System.Windows.Forms.CheckBox();
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exporterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize) (this.nudBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudMaxLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudMinLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbHost
            // 
            this.cbHost.FormattingEnabled = true;
            this.cbHost.Location = new System.Drawing.Point(89, 66);
            this.cbHost.Name = "cbHost";
            this.cbHost.Size = new System.Drawing.Size(134, 21);
            this.cbHost.TabIndex = 0;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Location = new System.Drawing.Point(263, 62);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(143, 20);
            this.dtpStartTime.TabIndex = 1;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(263, 97);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(143, 20);
            this.dtpEndTime.TabIndex = 2;
            // 
            // cbType
            // 
            this.cbType.DisplayMember = "Test";
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(91, 155);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(131, 21);
            this.cbType.TabIndex = 5;
            // 
            // tbRegex
            // 
            this.tbRegex.Location = new System.Drawing.Point(598, 157);
            this.tbRegex.Name = "tbRegex";
            this.tbRegex.Size = new System.Drawing.Size(241, 20);
            this.tbRegex.TabIndex = 8;
            // 
            // bValidate
            // 
            this.bValidate.Location = new System.Drawing.Point(663, 192);
            this.bValidate.Name = "bValidate";
            this.bValidate.Size = new System.Drawing.Size(174, 33);
            this.bValidate.TabIndex = 10;
            this.bValidate.Text = "Valider";
            this.bValidate.UseVisualStyleBackColor = true;
            this.bValidate.Click += new System.EventHandler(this.bValidate_Click);
            // 
            // lblHost
            // 
            this.lblHost.Location = new System.Drawing.Point(95, 42);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(126, 24);
            this.lblHost.TabIndex = 11;
            this.lblHost.Text = "Hôte";
            // 
            // lblLineBefore
            // 
            this.lblLineBefore.Location = new System.Drawing.Point(598, 31);
            this.lblLineBefore.Name = "lblLineBefore";
            this.lblLineBefore.Size = new System.Drawing.Size(126, 33);
            this.lblLineBefore.TabIndex = 14;
            this.lblLineBefore.Text = "Lignes à selectionner avant";
            // 
            // lblLineAfter
            // 
            this.lblLineAfter.Location = new System.Drawing.Point(740, 31);
            this.lblLineAfter.Name = "lblLineAfter";
            this.lblLineAfter.Size = new System.Drawing.Size(126, 33);
            this.lblLineAfter.TabIndex = 15;
            this.lblLineAfter.Text = "Lignes à selectionner après";
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(91, 128);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(126, 24);
            this.lblType.TabIndex = 16;
            this.lblType.Text = "Type d\'action";
            // 
            // lblMinLen
            // 
            this.lblMinLen.Location = new System.Drawing.Point(267, 129);
            this.lblMinLen.Name = "lblMinLen";
            this.lblMinLen.Size = new System.Drawing.Size(126, 24);
            this.lblMinLen.TabIndex = 17;
            this.lblMinLen.Text = "Longueur Minimale";
            // 
            // lblMaxLen
            // 
            this.lblMaxLen.Location = new System.Drawing.Point(447, 128);
            this.lblMaxLen.Name = "lblMaxLen";
            this.lblMaxLen.Size = new System.Drawing.Size(126, 24);
            this.lblMaxLen.TabIndex = 18;
            this.lblMaxLen.Text = "Longueur maximale";
            // 
            // lblRegex
            // 
            this.lblRegex.Location = new System.Drawing.Point(598, 128);
            this.lblRegex.Name = "lblRegex";
            this.lblRegex.Size = new System.Drawing.Size(126, 24);
            this.lblRegex.TabIndex = 19;
            this.lblRegex.Text = "Regex";
            // 
            // nudBefore
            // 
            this.nudBefore.Location = new System.Drawing.Point(598, 66);
            this.nudBefore.Maximum = new decimal(new int[] {10, 0, 0, 0});
            this.nudBefore.Name = "nudBefore";
            this.nudBefore.Size = new System.Drawing.Size(94, 20);
            this.nudBefore.TabIndex = 20;
            // 
            // nudMaxLen
            // 
            this.nudMaxLen.Location = new System.Drawing.Point(447, 155);
            this.nudMaxLen.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.nudMaxLen.Name = "nudMaxLen";
            this.nudMaxLen.Size = new System.Drawing.Size(94, 20);
            this.nudMaxLen.TabIndex = 21;
            this.nudMaxLen.Value = new decimal(new int[] {1000, 0, 0, 0});
            // 
            // nudMinLen
            // 
            this.nudMinLen.Location = new System.Drawing.Point(263, 157);
            this.nudMinLen.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.nudMinLen.Name = "nudMinLen";
            this.nudMinLen.Size = new System.Drawing.Size(94, 20);
            this.nudMinLen.TabIndex = 22;
            // 
            // nudAfter
            // 
            this.nudAfter.Location = new System.Drawing.Point(745, 66);
            this.nudAfter.Maximum = new decimal(new int[] {10, 0, 0, 0});
            this.nudAfter.Name = "nudAfter";
            this.nudAfter.Size = new System.Drawing.Size(94, 20);
            this.nudAfter.TabIndex = 23;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(86, 231);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(747, 506);
            this.dataGridView1.TabIndex = 24;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(447, 62);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(126, 20);
            this.dtpStartDate.TabIndex = 25;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(447, 97);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(126, 20);
            this.dtpEndDate.TabIndex = 26;
            // 
            // cbTime
            // 
            this.cbTime.Location = new System.Drawing.Point(263, 32);
            this.cbTime.Name = "cbTime";
            this.cbTime.Size = new System.Drawing.Size(160, 27);
            this.cbTime.TabIndex = 27;
            this.cbTime.Text = "Horaire de début et fin";
            this.cbTime.UseVisualStyleBackColor = true;
            this.cbTime.CheckedChanged += new System.EventHandler(this.cbTime_CheckedChanged);
            // 
            // cbDate
            // 
            this.cbDate.Location = new System.Drawing.Point(447, 37);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(125, 22);
            this.cbDate.TabIndex = 28;
            this.cbDate.Text = "Date de début et fin";
            this.cbDate.UseVisualStyleBackColor = true;
            this.cbDate.CheckedChanged += new System.EventHandler(this.cbDate_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.fichierToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(940, 24);
            this.menuStrip1.TabIndex = 29;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.exporterToolStripMenuItem, this.xMLToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // exporterToolStripMenuItem
            // 
            this.exporterToolStripMenuItem.Name = "exporterToolStripMenuItem";
            this.exporterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exporterToolStripMenuItem.Text = "Exporter";
            this.exporterToolStripMenuItem.Click += new System.EventHandler(this.exporterToolStripMenuItem_Click);
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xMLToolStripMenuItem.Text = "XML";
            this.xMLToolStripMenuItem.Click += new System.EventHandler(this.xMLToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 762);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.cbTime);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.nudAfter);
            this.Controls.Add(this.nudMinLen);
            this.Controls.Add(this.nudMaxLen);
            this.Controls.Add(this.nudBefore);
            this.Controls.Add(this.lblRegex);
            this.Controls.Add(this.lblMaxLen);
            this.Controls.Add(this.lblMinLen);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblLineAfter);
            this.Controls.Add(this.lblLineBefore);
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.bValidate);
            this.Controls.Add(this.tbRegex);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.cbHost);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize) (this.nudBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudMaxLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudMinLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem exporterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

        private System.Windows.Forms.CheckBox cbDate;
        private System.Windows.Forms.CheckBox cbTime;

        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartTime;

        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.NumericUpDown nudAfter;
        private System.Windows.Forms.NumericUpDown nudBefore;
        private System.Windows.Forms.NumericUpDown nudMaxLen;
        private System.Windows.Forms.NumericUpDown nudMinLen;

        private System.Windows.Forms.Label lblLineBefore;

        private System.Windows.Forms.Label lblLineAfter;
        private System.Windows.Forms.Label lblMaxLen;
        private System.Windows.Forms.Label lblMinLen;
        private System.Windows.Forms.Label lblRegex;
        private System.Windows.Forms.Label lblType;

        private System.Windows.Forms.Label lblHost;

        private System.Windows.Forms.Button bValidate;
        private System.Windows.Forms.TextBox tbRegex;

        private System.Windows.Forms.ComboBox cbType;

        private System.Windows.Forms.ComboBox cbHost;

        #endregion
    }
}