using System;
using AuthAPI.Globals;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AuthAPI
{
    /// <summary>
    /// Main class for AuthAPI
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point for AuthAPI
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            // setting Console title
            Console.Title = Constants.AuthAPI;

            // creating web host builder
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates Web Host Builder
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>Web Host Builder.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}