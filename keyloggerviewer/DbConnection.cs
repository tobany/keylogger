using System;
using System.Windows.Forms;

namespace keyloggerviewer
{
    public partial class DbConnection : Form
    {
        // Classe permettant d'obtenir les informations de connexion sql.
        public DbConnection()
        {
            InitializeComponent();
            label6.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lors de la validation, on vérifie si les informations de connexion sont valide. Si ce n'est pas le cas, on affiche le message d'erreur.
            try
            {
                DbLink db = DbLink.getInstance(this.tbIp.Text, this.tbUser.Text, this.tbPassword.Text, this.tbName.Text, this.tbPort.Text);
                Program.connection = db;
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                label6.Visible = true;
            }
        }
    }
}