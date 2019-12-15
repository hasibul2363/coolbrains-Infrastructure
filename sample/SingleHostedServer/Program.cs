using System;
using System.IO;
using System.Net.Mime;
using Microsoft.Extensions.Configuration;

namespace SingleHostedServer
{
    class Program
    {
        private static IConfiguration _configuration;
        private static void BuildConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }

        static void Main(string[] args)
        {
            BuildConfiguration();


            Console.WriteLine("Hello World!");
        }
    }
}
