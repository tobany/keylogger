using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Crypto.Agreement.Srp;

namespace keyloggerviewer
{
    public partial class Form1 : Form
    {
        private DbLink db;
        private List<LogData> logs;
        private string sortedColumn;
        public Form1(DbLink connection)
        {
            InitializeComponent();
            this.db = connection;
            List<string> names = db.getHostList();
            cbHost.Items.Add("Tous");
            foreach (string name in names)
            {
                cbHost.Items.Add(name);
            }
            
            cbHost.SelectedIndex = 0;
            cbHost.DropDownStyle = ComboBoxStyle.DropDownList;

            cbType.Items.Add(("Tous"));
            cbType.Items.Add(("text"));
            cbType.Items.Add(("copy"));
            cbType.Items.Add(("paste"));
            cbType.Items.Add(("click"));
            cbType.Items.Add(("shortcut"));
            cbType.SelectedIndex = 0;
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            logs = new List<LogData>();
            dataGridView1.DataSource = logs;
            
        }

        private void bValidate_Click(object sender, EventArgs e)
        {
            int nbLineAfter = Convert.ToInt32(nudAfter.Value);
            int nbLineBefore = Convert.ToInt32(nudBefore.Value);
            int contentMaxLen = Convert.ToInt32(nudMaxLen.Value);
            int contentMinLen = Convert.ToInt32(nudMinLen.Value);
            string regex = tbRegex.Text;
            string logType = cbType.SelectedItem.ToString();
            string hostName = cbHost.SelectedItem.ToString();
            if (hostName == "Tous")
            {
                hostName = "";
            }

            if (logType == "Tous")
            {
                logType = "";
            }
            
            this.logs = db.simpleGet(hostName: hostName, type: logType, regex: regex, lineAfter: nbLineAfter,
                lineBefore: nbLineBefore, contentMaxLen: contentMaxLen, contentMinLen: contentMinLen);
            List<LogData> ld = this.logs.OrderBy(o => o.HostName).ThenBy(o => o.PrivateIp).ThenBy(o => o.PublicIp).ThenBy(o => o.LogId).ToList();;
            dataGridView1.DataSource = ld;
            dataGridView1.Update();
            dataGridView1.Refresh();
            sortedColumn = "HostName";
            dataGridView1.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            


        }

        private void dataGridView1_ColumnHeaderMouseClick(
            object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridView1.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridView1.SortedColumn;
            ListSortDirection direction;
            string head = newColumn.HeaderText;
            if (head == this.sortedColumn)
            {
                
            }

/*
            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridView1.SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }
*/
            // Sort the selected column.
            //dataGridView1.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
        }
        
    }
}