using System.ComponentModel;

namespace keyloggerviewer
{
    partial class DbConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(62, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connexion à la base de donnée MySQL";
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(62, 75);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(125, 20);
            this.tbIp.TabIndex = 1;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(233, 75);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(126, 20);
            this.tbPort.TabIndex = 2;
            this.tbPort.Text = "3306";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(410, 74);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(124, 20);
            this.tbName.TabIndex = 3;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(62, 126);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(124, 20);
            this.tbUser.TabIndex = 4;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(233, 126);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(124, 20);
            this.tbPassword.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(63, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Adresse ip du serveur";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(233, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 31);
            this.label3.TabIndex = 7;
            this.label3.Text = "Port utilisé (par défaut 3306)";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(410, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 30);
            this.label4.TabIndex = 8;
            this.label4.Text = "Nom de la base de données";
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(65, 101);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(122, 22);
            this.Label5.TabIndex = 9;
            this.Label5.Text = "Nom d\'utilisateur";
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(233, 101);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(122, 22);
            this.label.TabIndex = 10;
            this.label.Text = "Mot de passe";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(419, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 31);
            this.button1.TabIndex = 11;
            this.button1.Text = "Valider";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DbConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.label1);
            this.Name = "DbConnection";
            this.Text = "DbConnection";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label Label5;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbUser;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}