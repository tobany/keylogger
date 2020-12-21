﻿namespace keyloggerviewer
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
            ((System.ComponentModel.ISupportInitialize) (this.nudBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudMaxLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudMinLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbHost
            // 
            this.cbHost.FormattingEnabled = true;
            this.cbHost.Location = new System.Drawing.Point(86, 33);
            this.cbHost.Name = "cbHost";
            this.cbHost.Size = new System.Drawing.Size(134, 21);
            this.cbHost.TabIndex = 0;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Location = new System.Drawing.Point(260, 29);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(143, 20);
            this.dtpStartTime.TabIndex = 1;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(260, 64);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(143, 20);
            this.dtpEndTime.TabIndex = 2;
            // 
            // cbType
            // 
            this.cbType.DisplayMember = "Test";
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(88, 122);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(131, 21);
            this.cbType.TabIndex = 5;
            // 
            // tbRegex
            // 
            this.tbRegex.Location = new System.Drawing.Point(595, 124);
            this.tbRegex.Name = "tbRegex";
            this.tbRegex.Size = new System.Drawing.Size(241, 20);
            this.tbRegex.TabIndex = 8;
            // 
            // bValidate
            // 
            this.bValidate.Location = new System.Drawing.Point(660, 159);
            this.bValidate.Name = "bValidate";
            this.bValidate.Size = new System.Drawing.Size(174, 33);
            this.bValidate.TabIndex = 10;
            this.bValidate.Text = "Valider";
            this.bValidate.UseVisualStyleBackColor = true;
            this.bValidate.Click += new System.EventHandler(this.bValidate_Click);
            // 
            // lblHost
            // 
            this.lblHost.Location = new System.Drawing.Point(92, 9);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(126, 24);
            this.lblHost.TabIndex = 11;
            this.lblHost.Text = "Hôte";
            // 
            // lblLineBefore
            // 
            this.lblLineBefore.Location = new System.Drawing.Point(595, -2);
            this.lblLineBefore.Name = "lblLineBefore";
            this.lblLineBefore.Size = new System.Drawing.Size(126, 33);
            this.lblLineBefore.TabIndex = 14;
            this.lblLineBefore.Text = "Lignes à selectionner avant";
            // 
            // lblLineAfter
            // 
            this.lblLineAfter.Location = new System.Drawing.Point(737, -2);
            this.lblLineAfter.Name = "lblLineAfter";
            this.lblLineAfter.Size = new System.Drawing.Size(126, 33);
            this.lblLineAfter.TabIndex = 15;
            this.lblLineAfter.Text = "Lignes à selectionner après";
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(88, 95);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(126, 24);
            this.lblType.TabIndex = 16;
            this.lblType.Text = "Type d\'action";
            // 
            // lblMinLen
            // 
            this.lblMinLen.Location = new System.Drawing.Point(264, 96);
            this.lblMinLen.Name = "lblMinLen";
            this.lblMinLen.Size = new System.Drawing.Size(126, 24);
            this.lblMinLen.TabIndex = 17;
            this.lblMinLen.Text = "Longueur Minimale";
            // 
            // lblMaxLen
            // 
            this.lblMaxLen.Location = new System.Drawing.Point(444, 95);
            this.lblMaxLen.Name = "lblMaxLen";
            this.lblMaxLen.Size = new System.Drawing.Size(126, 24);
            this.lblMaxLen.TabIndex = 18;
            this.lblMaxLen.Text = "Longueur maximale";
            // 
            // lblRegex
            // 
            this.lblRegex.Location = new System.Drawing.Point(595, 95);
            this.lblRegex.Name = "lblRegex";
            this.lblRegex.Size = new System.Drawing.Size(126, 24);
            this.lblRegex.TabIndex = 19;
            this.lblRegex.Text = "Regex";
            // 
            // nudBefore
            // 
            this.nudBefore.Location = new System.Drawing.Point(595, 33);
            this.nudBefore.Maximum = new decimal(new int[] {10, 0, 0, 0});
            this.nudBefore.Name = "nudBefore";
            this.nudBefore.Size = new System.Drawing.Size(94, 20);
            this.nudBefore.TabIndex = 20;
            // 
            // nudMaxLen
            // 
            this.nudMaxLen.Location = new System.Drawing.Point(444, 122);
            this.nudMaxLen.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.nudMaxLen.Name = "nudMaxLen";
            this.nudMaxLen.Size = new System.Drawing.Size(94, 20);
            this.nudMaxLen.TabIndex = 21;
            this.nudMaxLen.Value = new decimal(new int[] {1000, 0, 0, 0});
            // 
            // nudMinLen
            // 
            this.nudMinLen.Location = new System.Drawing.Point(260, 124);
            this.nudMinLen.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.nudMinLen.Name = "nudMinLen";
            this.nudMinLen.Size = new System.Drawing.Size(94, 20);
            this.nudMinLen.TabIndex = 22;
            // 
            // nudAfter
            // 
            this.nudAfter.Location = new System.Drawing.Point(742, 33);
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
            this.dataGridView1.Size = new System.Drawing.Size(747, 390);
            this.dataGridView1.TabIndex = 24;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(444, 29);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(126, 20);
            this.dtpStartDate.TabIndex = 25;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(444, 64);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(126, 20);
            this.dtpEndDate.TabIndex = 26;
            // 
            // cbTime
            // 
            this.cbTime.Location = new System.Drawing.Point(260, -1);
            this.cbTime.Name = "cbTime";
            this.cbTime.Size = new System.Drawing.Size(160, 27);
            this.cbTime.TabIndex = 27;
            this.cbTime.Text = "Horaire de début et fin";
            this.cbTime.UseVisualStyleBackColor = true;
            this.cbTime.CheckedChanged += new System.EventHandler(this.cbTime_CheckedChanged);
            // 
            // cbDate
            // 
            this.cbDate.Location = new System.Drawing.Point(444, 4);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(125, 22);
            this.cbDate.TabIndex = 28;
            this.cbDate.Text = "Date de début et fin";
            this.cbDate.UseVisualStyleBackColor = true;
            this.cbDate.CheckedChanged += new System.EventHandler(this.cbDate_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 646);
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
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize) (this.nudBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudMaxLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudMinLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

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