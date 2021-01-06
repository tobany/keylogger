using System;
using System.Windows.Forms;

namespace keyloggerviewer
{
    public partial class FtpConnection : Form
    {
        // Fenêtre utilisé pour obtenir les informations de connexion ftp nécessaire à la récuperation des données
        public FtpConnection()
        {
            // On récupère les infos de connxion enregistré si il y en a.
            InitializeComponent();
            tbIp.Text = DataAdd.ip;
            tbPort.Text = DataAdd.port;
            tbUser.Text = DataAdd.username2;
            tbPassword.Text = DataAdd.password;
            label4.Visible = false;
        }

        private void bValidate_Click(object sender, EventArgs e)
        {
            // Lors de la validation, on test la connexion. Si elle est valide, on charge les données, sinon on affiche le message d'erreur.
            string ip = tbIp.Text;
            string port = tbPort.Text;
            string username = tbUser.Text;
            string pwd = tbPassword.Text;
            string folder = tbFolder.Text;
            if (DataAdd.checkConnection(ip, port, username, pwd, ""))
            {
                DataAdd.loadData();
                this.Close();
            }
            else
                label4.Visible = true;
        }
        
    }
}