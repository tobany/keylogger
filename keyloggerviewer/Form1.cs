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
        private bool ascendSort;
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
            ascendSort = true;
            sortedColumn = "HostName";
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
            List<LogData> ld = this.logs.OrderBy(o => o.HostName).ThenBy(o => o.LogId).ToList();;
            dataGridView1.DataSource = ld;
            dataGridView1.Update();
            dataGridView1.Refresh();
            sortedColumn = "HostName";
            dataGridView1.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            
            


        }

        private void dataGridView1_ColumnHeaderMouseClick(
            object sender, DataGridViewCellMouseEventArgs e)
        {
            List<LogData> ld = this.logs;
            DataGridViewColumn newColumn = dataGridView1.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridView1.Columns[this.sortedColumn];
            string head = newColumn.HeaderText;
            bool ok = false;
            if (head == this.sortedColumn && ascendSort)
            {
                Console.WriteLine("NOPE");
                switch (head)
                {
                    case "LogId":
                        ld = this.logs.OrderBy(o => o.HostName).ThenByDescending(o => o.LogId).ToList();
                        ok = true;
                        break;
                    case "HostName":
                        ld = this.logs.OrderByDescending(o => o.HostName).ThenBy(o => o.LogId).ToList();
                        ok = true;
                        break;
                    case "Date":
                        ld = this.logs.OrderByDescending(o => o.Date).ThenBy(o => o.HostName).ThenBy(o => o.LogId)
                            .ToList();
                        ok = true;
                        break;
                    case "Type":
                        ld = this.logs.OrderByDescending(o => o.Type).ThenBy(o => o.HostName).ThenBy(o => o.LogId)
                            .ToList();
                        ok = true;
                        break;
                }

                ascendSort = false;
                newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
            }
            else
            {
                Console.WriteLine("OKK");
                switch (head)
                {
                    case "LogId":
                        ld = this.logs.OrderBy(o => o.HostName).ThenBy(o => o.LogId).ToList();
                        ok = true;
                        break;
                    case "HostName":
                        ld = this.logs.OrderBy(o => o.HostName).ThenBy(o => o.LogId).ToList();
                        ok = true;
                        break;
                    case "Date":
                        ld = this.logs.OrderBy(o => o.Date).ThenBy(o => o.HostName).ThenBy(o => o.LogId)
                            .ToList();
                        ok = true;
                        break;
                    case "Type":
                        ld = this.logs.OrderBy(o => o.Type).ThenBy(o => o.HostName).ThenBy(o => o.LogId)
                            .ToList();
                        ok = true;
                        break;
                }

                if (ok)
                {
                    ascendSort = true;
                    
                }

            }

            if (ok)
            {
                dataGridView1.DataSource = ld;
                dataGridView1.Columns[this.sortedColumn].HeaderCell.SortGlyphDirection = SortOrder.None;
                if (ascendSort)
                    dataGridView1.Columns[head].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                else
                    dataGridView1.Columns[head].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                sortedColumn = head;
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
        }
        
    }
}