using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080/";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Server is runing at {url}");
                Console.ReadLine();
            }
        }
    }
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR("/signalrchat",new HubConfiguration());

            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null; 
        }
    }
}
