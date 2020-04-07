using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotkeyLib_Auth_Server.function.Generator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HotkeyLib_Auth_Server
{
    public class Program
    {
        public static DataBase DataBase;
        public static void Main(string[] args)
        {
            var a =HashGenerator.ComputeSha512Hash("asdf", 100);
            DataBase = new DataBase("DataBase");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
