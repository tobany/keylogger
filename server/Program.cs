using System;

using System.Threading;
using System.Threading.Tasks;
using System.Net;
 
namespace KeyLogger
{
    class MainClass
    {
        private static readonly AutoResetEvent _closingEvent = new AutoResetEvent(false);
 
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Docker World!");
            var count = 0;
 
            Task.Factory.StartNew(() =>
            {
                HttpListener http = new HttpListener();
                http.Prefixes.Add("http://*:8094/toto/");
                http.Start();
                while (true)
                {
                    HttpListenerContext ctx = http.GetContext();
                    HttpListenerRequest rqst = ctx.Request;
                    Console.WriteLine(rqst.Url.OriginalString);
                    Console.WriteLine(rqst.Headers);
                }
            });
            
            Console.CancelKeyPress += ((s, a) =>
            {
                Console.WriteLine("Bye!");
                _closingEvent.Set();
            });
 
            _closingEvent.WaitOne();
        }
    }
}
