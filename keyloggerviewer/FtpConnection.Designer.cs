using System.ComponentModel;

namespace keyloggerviewer
{
    partial class FtpConnection
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
            this.bValidate = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bValidate
            // 
            this.bValidate.Location = new System.Drawing.Point(27, 190);
            this.bValidate.Name = "bValidate";
            this.bValidate.Size = new System.Drawing.Size(136, 31);
            this.bValidate.TabIndex = 23;
            this.bValidate.Text = "Valider";
            this.bValidate.UseVisualStyleBackColor = true;
            this.bValidate.Click += new System.EventHandler(this.bValidate_Click);
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(198, 105);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(122, 22);
            this.label.TabIndex = 22;
            this.label.Text = "Mot de passe";
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(30, 105);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(122, 22);
            this.Label5.TabIndex = 21;
            this.Label5.Text = "Nom d\'utilisateur";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(198, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 31);
            this.label3.TabIndex = 19;
            this.label3.Text = "Port utilisé (par défaut 21)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(28, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 22);
            this.label2.TabIndex = 18;
            this.label2.Text = "Adresse ip du serveur";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(198, 130);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(126, 20);
            this.tbPassword.TabIndex = 17;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(27, 130);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(124, 20);
            this.tbUser.TabIndex = 16;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(198, 79);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(126, 20);
            this.tbPort.TabIndex = 14;
            this.tbPort.Text = "21";
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(27, 79);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(125, 20);
            this.tbIp.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "Connexion au serveur Ftp pour charger les données dans la base de donnée mysql";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(27, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(459, 34);
            this.label4.TabIndex = 24;
            this.label4.Text = "Connexion impossible ! Veuillez vérifier les informations saisies.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(385, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 30);
            this.label6.TabIndex = 25;
            this.label6.Text = "Dossier";
            // 
            // tbFolder
            // 
            this.tbFolder.Location = new System.Drawing.Point(360, 79);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(126, 20);
            this.tbFolder.TabIndex = 26;
            // 
            // FtpConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 259);
            this.Controls.Add(this.tbFolder);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bValidate);
            this.Controls.Add(this.label);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.label1);
            this.Name = "FtpConnection";
            this.Text = "Mise à jour de la base de donnée";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox tbFolder;

        private System.Windows.Forms.Label label6;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Button bValidate;

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbUser;

        #endregion
    }
}