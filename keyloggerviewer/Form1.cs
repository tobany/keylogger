using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Crypto.Agreement.Srp;

namespace keyloggerviewer
{
    public partial class Form1 : Form
    {
        private DbLink db;
        private List<LogData> logs;
        private string sortedColumn;
        private bool ascendSort;
        public Form1()
        {
            InitializeComponent();
            var a = new DbConnection();
            a.ShowDialog(); 
            while (Program.connection is null)
            {
                string message = "Une base de donnée mySQL est requise pour cette application.\n Voulez vous quitter ?";
                string caption = "Erreur connexion mysql";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    // Closes the parent form.
                    Application.Exit();
                    this.Close();
                    return;
                }
                else
                {
                    a.ShowDialog(); 
                }
            }
            this.db = Program.connection;
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
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.ShowUpDown = true;
            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.ShowUpDown = true;
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpStartTime.Enabled = false;
            dtpEndTime.Enabled = false;
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
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
            string startDate = "";
            string endDate = "";
            string startTime = "";
            string endTime = "";
            if (cbTime.Checked)
            {
                startTime = dtpStartTime.Text;
                endTime = dtpEndTime.Text;
            }
            if (cbDate.Checked)
            {
                startDate = dtpStartDate.Value.ToString("yyyy-MM-dd");
                endDate = dtpEndDate.Value.ToString("yyyy-MM-dd");
            }

            if (hostName == "Tous")
            {
                hostName = "";
            }

            if (logType == "Tous")
            {
                logType = "";
            }
            
            this.logs = db.simpleGet(hostName: hostName, type: logType, regex: regex, lineAfter: nbLineAfter,
                lineBefore: nbLineBefore, contentMaxLen: contentMaxLen, contentMinLen: contentMinLen, endDate: endDate,
                startDate: startDate, endTime: endTime, startTime: startTime);
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

        private void cbTime_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTime.Checked)
            {
                dtpStartTime.Enabled = true;
                dtpEndTime.Enabled = true;
            }
            else
            {
                dtpStartTime.Enabled = false;
                dtpEndTime.Enabled = false;
            }
        }

        private void cbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDate.Checked)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }
        }

        private void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.logs.Count > 0)
            {
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != null)
                    {
                        StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                        sw.WriteLine("HostName, LogId, PrivateIp, PublicIp, Date, Type, Content");
                        foreach (LogData log in logs)
                        {
                            sw.WriteLine(log.ToString());
                        }

                        sw.Dispose();
                    }
                }
                saveFileDialog1.Dispose();
            }
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // saveFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            // saveFileDialog1.FilterIndex = 2;
            // saveFileDialog1.RestoreDirectory = true;
            //
            // if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            // {
            //     if (saveFileDialog1.FileName != null)
            //     {
            //         XmlSerializer serializer = new XmlSerializer(logs.GetType(), logs); 
            //         FileStream fs = new FileStream("Personenliste.xml", FileMode.Create); 
            //         serializer.Serialize(fs, logs);
            //         fs.Close();
            //     }
            // }
        }

        private void chargerDepuisDtpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataAdd.ConnectionServer();
        }
    }
}