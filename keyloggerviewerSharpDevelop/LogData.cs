using System;

namespace keyloggerviewer
{
    // Classe représentant les logs pour l'affichage sur le ficher. Possède aussi comme attribut le nom de l'hote pur pouvoir l'afficher.
    [Serializable]
    public class LogData
    {
        private string hostName;
        private string appWindow;
        private string publicIp;
        private DateTime date;
        private string type;
        private string content;
        private int logId;

        public int LogId
        {
        	get {return logId;}
        	set {logId = value;}
        }

        public string HostName
        {
        	get {return hostName;}
    		set {hostName = value;}
        }

        public string AppWindow
        {
        	get {return appWindow;}
    		set {appWindow = value;}
        }

        public string PublicIp
        {
        	get {return publicIp;}
    		set {publicIp = value;}
        }

        public DateTime Date
        {
        	get {return date;}
    		set {date = value;}
        }

        public string Type
        {
        	get {return type;}
    		set {type = value;}
        }

        public string Content
        {
        	get {return content;}
    		set {content = value;}
        }

        public LogData(string hostName, string appWindow, string publicIp, DateTime date, string type, string content, int logId)
        {
            this.hostName = hostName;
            this.appWindow = appWindow;
            this.publicIp = publicIp;
            this.date = date;
            this.type = type;
            this.content = content;
            this.logId = logId;
        }

        public override string ToString()
        {
            return this.hostName + ", " + this.logId + ", " + this.publicIp + ", " + this.appWindow + ", " + this.date +
                   ", " + this.type + ", " + this.content;
        }
    }
}