using System;

namespace keyloggerviewer
{
    [Serializable]
    public class LogData
    {
        private string hostName;
        private string publicIp;
        private string privateIp;
        private DateTime date;
        private string type;
        private string content;
        private int logId;

        public int LogId
        {
            get => logId;
            set => logId = value;
        }

        public string HostName
        {
            get => hostName;
            set => hostName = value;
        }

        public string PublicIp
        {
            get => publicIp;
            set => publicIp = value;
        }

        public string PrivateIp
        {
            get => privateIp;
            set => privateIp = value;
        }

        public DateTime Date
        {
            get => date;
            set => date = value;
        }

        public string Type
        {
            get => type;
            set => type = value;
        }

        public string Content
        {
            get => content;
            set => content = value;
        }

        public LogData(string hostName, string publicIp, string privateIp, DateTime date, string type, string content, int logId)
        {
            this.hostName = hostName;
            this.publicIp = publicIp;
            this.privateIp = privateIp;
            this.date = date;
            this.type = type;
            this.content = content;
            this.logId = logId;
        }

        public override string ToString()
        {
            return this.hostName + ", " + this.logId + ", " + this.publicIp + ", " + this.privateIp + ", " + this.date +
                   ", " + this.type + ", " + this.content;
        }
    }
}