using System;
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

        
        public static DbLink  getInstance(string server, string user, string password, string dbName, string port)
        {
            // permet de récupérer une connexion sql ou de la créer si ce n'est pas encore le cas.
            if (link != null)
            {
                link = new DbLink(server, user, password, dbName, port);
            }

            return link;
        }
        
        private DbLink(string server, string user, string password, string dbName, string port)
        {
            string connStr = "server=" + server + ";user=" + user + ";database=" + dbName + ";port=" + port + ";password=" + password;
            this.connection = new MySqlConnection(connStr);
            this.connection.Open();
            this.connection.Close();
        }

        public List<LogData> simpleGet(string hostName = "", string hostPublicIp = "", string hostPrivateIp = "",
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
                    @"SELECT h.hostName, h.hostPublicIp, h.hostPrivateIp, TIMESTAMP(l.logDate, l.logTime), l.logType, l.content, l.logId
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
                "INSERT INTO log (hostId, logId, logType, logDate, logTime, content) VALUES (@hostId, @logId, @logType, @logDate, @logTime, @content)";
            MySqlCommand req = new MySqlCommand(sqlReq, connection);
            req.Parameters.Add("@hostId", MySqlDbType.Int32);
            req.Parameters.Add("@logId", MySqlDbType.Int32);
            req.Parameters.Add("@logType", MySqlDbType.String);
            req.Parameters.Add("@logDate", MySqlDbType.DateTime);
            req.Parameters.Add("@logTime", MySqlDbType.DateTime);
            req.Parameters.Add("@content", MySqlDbType.String);
            foreach (Log log in h.LogList)
            {
                req.Parameters["@hostId"].Value = log.HostId;
                req.Parameters["@logId"].Value = log.LogId;
                req.Parameters["@logType"].Value = log.LogType;
                req.Parameters["@logDate"].Value = log.LogDate;
                req.Parameters["@logTime"].Value = log.LogTime;
                req.Parameters["@content"].Value = log.LogContent;
                req.ExecuteNonQuery();
            }
            
            connection.Close();
            return 1;
        }
    }
}