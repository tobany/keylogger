using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;


namespace keyloggerviewer
{
    public class DbLink
    {
        private MySqlConnection connection;

        public DbLink()
        {
            string connStr = "server=localhost;user=keylogger;database=keylogger;port=3306;password=keylogger";
            this.connection = new MySqlConnection(connStr);
        }
        
        public List<LogData> simpleGet(string hostName="", string hostPublicIp="", string hostPrivateIp="",
            string type="", int contentMaxLen=1000, int contentMinLen=0, string regex="", int lineBefore=0, 
            int lineAfter=0, string startDate="", string endDate="", string startTime="", string endTime="")
        {
            List<LogData> logList = new List<LogData>();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            string sqlReq = @"SELECT l.hostId, l.logId 
            FROM host as h JOIN log as l ON h.hostId = l.hostId
            WHERE (@hostname = '' OR h.hostName = @hostname) 
            AND (@publicip = '' OR h.hostPublicIp = @publicip)
            AND (@privateip = '' OR h.hostPrivateIp = @privateip)
            AND (@type = '' OR l.logType = @type)
            AND (@contentmaxlen = 0 OR l.content IS NULL OR CHAR_LENGTH(l.content) < @contentmaxlen)
            AND (@contentminlen = 0 OR l.content IS NULL OR CHAR_LENGTH (l.content) > @contentminlen)
            AND (@regex = '' OR l.content IS NULL OR l.content REGEXP @regex)
            AND (@startTime = '' OR l.logTime >= @startTime)
            AND (@endTime = '' OR l.logTime <= @endTime)
            AND (@startDate = '' OR l.logDate >= @startDate)
            AND (@endDate = '' OR l.logDate <= @endDate)";
            MySqlCommand req = new MySqlCommand(sqlReq, connection);
            req.Parameters.AddWithValue("@hostname", hostName);
            req.Parameters.AddWithValue("@publicip", hostPublicIp);
            req.Parameters.AddWithValue("@privateip", hostPrivateIp);
            req.Parameters.AddWithValue("@type", type);
            req.Parameters.AddWithValue("@contentmaxlen", contentMaxLen);
            req.Parameters.AddWithValue("@contentminlen", contentMinLen);
            req.Parameters.AddWithValue("@startTime", startTime);
            req.Parameters.AddWithValue("@endTime", endTime);
            req.Parameters.AddWithValue("@startDate", startDate);
            req.Parameters.AddWithValue("@endDate", endDate);
            req.Parameters.AddWithValue("@regex", regex);
            MySqlDataReader rdr = req.ExecuteReader();
            List<int[]> idCombo = new List<int[]>();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0]+" -- "+rdr[1]);
                int[] a = {int.Parse(rdr[0].ToString()), int.Parse(rdr[1].ToString())};
                idCombo.Add(a);
            }
            rdr.Close();
            foreach (int[] i in idCombo)
            {
                Console.WriteLine(i[0]);
                Console.WriteLine(i[1]);
                sqlReq = @"SELECT h.hostName, h.hostPublicIp, h.hostPrivateIp, TIMESTAMP(l.logDate, l.logTime), l.logType, l.content, l.logId
                            FROM host as h JOIN log as l ON h.hostId = l.hostId
                            WHERE (l.logId <= @maxLog) AND (l.logId >= @minLog) AND (l.hostId=@hostId)";
                req = new MySqlCommand(sqlReq, connection);
                req.Parameters.AddWithValue("@hostId", i[0]);
                req.Parameters.AddWithValue("@minLog", i[1] - lineBefore);
                req.Parameters.AddWithValue("@maxLog", i[1] + lineAfter);
                MySqlDataReader rdr2 = req.ExecuteReader();
                while (rdr2.Read())
                {
                    Console.WriteLine(rdr2[0] + " -- " + rdr2[1] + " -- " + rdr2[2] + " -- " + rdr2[3] + " -- " +
                                      rdr2[4] + " -- " + rdr2[5] + " -- " + rdr2[6]);
                    logList.Add(new LogData(rdr2[0].ToString(), rdr2[1].ToString(), rdr2[2].ToString(), DateTime.Parse(rdr2[3].ToString()),rdr2[4].ToString(),rdr2[5].ToString(), int.Parse(rdr2[6].ToString())));
                    
                }
                rdr2.Close();
            }
                
            connection.Close();
            Console.WriteLine("OK");
            
            return logList;
        }

        public List<String> getHostList()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            string sqlReq = @"SELECT DISTINCT hostName FROM host";
            MySqlCommand req = new MySqlCommand(sqlReq, connection);
            MySqlDataReader rdr = req.ExecuteReader();
            List<string> names = new List<string>();
            while (rdr.Read())
            {
                names.Add(rdr[0].ToString());              
            }
            rdr.Close();
            return names;
        }

    }
}