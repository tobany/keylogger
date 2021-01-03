using System;
using System.Windows.Forms;
using Org.BouncyCastle.Bcpg;

namespace keyloggerviewer
{
    public partial class DbConnection : Form
    {
        // Classe permettant d'obtenir les informations de connexion sql.
        public DbConnection()
        {
            InitializeComponent();
            label6.Visible = false;
            label7.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lors de la validation, on vérifie si les informations de connexion sont valide. Si ce n'est pas le cas, on affiche le message d'erreur.
            try
            {
                if (!cbInit.Checked)
                {
                    DbLink db = DbLink.getInstance(this.tbIp.Text, this.tbUser.Text, this.tbPassword.Text, this.tbPort.Text,
                        this.tbName.Text);
                    Program.connection = db;
                    this.Close();
                }
                else
                {
                    DbLink db = DbLink.createInstance(this.tbIp.Text, this.tbUser.Text, this.tbPassword.Text, this.tbPort.Text,
                        this.tbName.Text);
                    if (db != null)
                    {
                        Program.connection = db;
                        this.Close(); 
                    }
                    else
                    {
                        label6.Visible = false;
                        label7.Visible = true;
                    }
                }
                    
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                label6.Visible = true;
                label7.Visible = false;
            }
        }
    }
}