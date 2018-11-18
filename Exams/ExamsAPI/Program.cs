/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Main class of Exams API 
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ExamsAPI
{
    /// <summary>
    /// Program 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of ExamsAPI
        /// </summary>
        /// <param name="args">Cmd arguments</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates web host builder
        /// </summary>
        /// <param name="args">Cmd arguments</param>
        /// <returns>web host builder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}