using System;

namespace keyloggerviewer
{
    //Classe représentant les logs lors de l'insértion en base de données
    public class Log
    {
        private int hostId;
        private int logId;
        private DateTime logDate;
        private DateTime logTime;
        private string logType;
        private string logContent;
        private string logApp;

        public Log(DateTime logDate, DateTime logTime, string logType, string logApp, string logContent="")
        {
            this.logDate = logDate;
            this.logTime = logTime;
            this.logType = logType;
            this.logContent = logContent;
            this.logApp = logApp;
        }

        public int HostId
        {
            get => hostId;
            set => hostId = value;
        }

        public int LogId
        {
            get => logId;
            set => logId = value;
        }

        public DateTime LogDate
        {
            get => logDate;
            set => logDate = value;
        }

        public DateTime LogTime
        {
            get => logTime;
            set => logTime = value;
        }

        public string LogType
        {
            get => logType;
            set => logType = value;
        }

        public string LogContent
        {
            get => logContent;
            set => logContent = value;
        }

        public string LogApp
        {
            get => logApp;
            set => logApp = value;
        }

        public override string ToString()
        {
            return logDate + "--" + logApp + "--" + logType + "--" + LogContent;
            
        }
    }
}