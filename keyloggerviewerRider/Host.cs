using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace keyloggerviewer
{
    //Classe représentant un poste ayant des log lors de l'insertion en base de donnée depuis le serveur ftp
    public class Host
    {
        private string publicIp;
        private string hostName;
        private int sqlId;
        private List<Log> logList;

        public Host(string publicIp, string hostName)
        {
            this.publicIp = publicIp;
            this.hostName = hostName;
            this.logList = new List<Log>();
            this.SqlId = -1;
        }

        public string PublicIp
        {
            get => publicIp;
            set => publicIp = value;
        }

        public string HostName
        {
            get => hostName;
            set => hostName = value;
        }

        public int SqlId
        {
            get => sqlId;
            set
            {
                sqlId = value;
                foreach (Log log in logList)
                {
                    log.HostId = value;
                }
            }
        }

        public List<Log> LogList
        {
            get
            {
                foreach (Log log in logList)
                {
                    log.HostId = sqlId;
                }

                return logList;
            }
            set => logList = value;
        }

        public void addLog(Log log)
        {
            logList.Add(log);
        }

        public override string ToString()
        {
            string val = publicIp + " " + hostName + " :\n";
            foreach (Log log in logList)
            {
                val += log + "\n";
            }

            return val;
        }
    }
}