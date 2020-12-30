using System;
using System.Windows.Forms;

namespace keyloggerviewer
{
    public partial class DbConnection : Form
    {
        public DbConnection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DbLink db = new DbLink(this.tbIp.Text, this.tbUser.Text, this.tbPassword.Text, this.tbName.Text, this.tbPort.Text);
                Program.connection = db;
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}