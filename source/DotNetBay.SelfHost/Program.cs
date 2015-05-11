using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.WebApi.Controllers;
using Microsoft.Owin.Hosting;

namespace DotNetBay.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {

            var typesLoaded = new[] { typeof(StatusController), typeof(AuctionController), typeof(SqlProviderServices) };

            string baseAddress = "http://localhost:9001/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Application now hosted and available at: " + baseAddress);
                Console.ReadLine(); 
            }

        }
    }
}
