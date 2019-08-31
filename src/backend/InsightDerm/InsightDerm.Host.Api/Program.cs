using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace InsightDerm.Host.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseIISIntegration()
                //.UseUrls("http://localhost:5000", "http://172.20.10.3:5000")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
