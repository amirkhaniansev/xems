/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class for Exams API Startup operations
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XemsLogger;
using XemsMailer.Mailers;
using ExamsAPI.Global;
using ExamsAPI.Repositories;

namespace ExamsAPI
{
    /// <summary>
    /// Class for startup operation of ExamsAPI
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Main configuration of ExamsAPI
        /// </summary>
        private IConfiguration Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

        /// <summary>
        /// Credentials for xems-mailing@gmail.com
        /// </summary>
        private IConfiguration Credentials = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("MailCredentials.json").Build();

        /// <summary>
        /// Configures services.
        /// This method gets called by the runtime which uses this method to add services to the container.
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // adding MVC Core,authorization and JSON formatting
            services.AddMvcCore()
                    .AddAuthorization()
                    .AddJsonFormatters();

            // adding authentication info
            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = this.Configuration["Endpoints:AuthAPI"];
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "ExamsAPI";
                    });

            // adding policies
            this.AddPolicies(services);

            // adding globals
            this.InitGlobals();
        }

        /// <summary>
        /// Configures app and environment.
        /// This method gets called by the runtime which uses this method to configure
        /// the HTTP request pipeline.
        /// </summary>
        /// <param name="app">App</param>
        /// <param name="env">Environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }

        /// <summary>
        /// Adds policies
        /// </summary>
        /// <param name="services">Services</param>
        private void AddPolicies(IServiceCollection services)
        {
            services.AddAuthorization(
                options => options.AddPolicy("IsLecturer", 
                    policy => policy.RequireClaim("current_profile", "lecturer"))
                );

            services.AddAuthorization(
                options => options.AddPolicy("IsStudent",
                    policy => policy.RequireClaim("current_profile", "student")));
        }

        /// <summary>
        /// Adds globals
        /// </summary>
        private void InitGlobals()
        {
            Globals.Logger = new Logger(
                this.Configuration[Constants.AppName], this.Configuration[Constants.LogsFile]);

            Globals.Mailer = new MessageMailer(
                new NetworkCredential(
                    this.Credentials[Constants.Username], 
                    this.Credentials[Constants.Password]),
                    this.Credentials[Constants.Username]);

            Globals.ExamRepository = new ExamRepository(
                new MongoDbSetting
                {
                    ConnectionString = this.Configuration[Constants.MongoDbConnectionString],
                    DatabaseName = this.Configuration[Constants.MongoDbDatabaseName]
                });

            Globals.ExamAnswerRepository = new ExamAnswerRepository(
                new MongoDbSetting
                {
                    ConnectionString = this.Configuration[Constants.MongoDbConnectionString],
                    DatabaseName = this.Configuration[Constants.MongoDbDatabaseName]
                });
        }
    }
}