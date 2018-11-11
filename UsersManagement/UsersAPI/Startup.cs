using System.IO;
using System.Net;
using AccessCore.Repository;
using AccessCore.SpExecuters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XemsLogger;
using XemsMailer.Mailers;
using XemsPasswordHash;

namespace UsersAPI
{
    /// <summary>
    /// Startup class for User Management API
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

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
                        options.ApiName = "UsersAPI";
                    });

            // adding policies
            this.AddPolicies(services);

            // adding globals
            this.AddGlobals();
        }

        /// <summary>
        /// Configures app and environment.
        /// This method gets called by the runtime which uses this method to configure the HTTP request pipeline.
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
            
        }

        /// <summary>
        /// Adds globals
        /// </summary>
        private void AddGlobals()
        {
            Globals.Logger = new Logger(
                this.Configuration["AppName"], this.Configuration["LogsFile"]);

            Globals.Mailer = new MessageMailer(
                new NetworkCredential(this.Credentials["Username"], this.Credentials["Password"]),
                this.Credentials["Username"]);

            Globals.DataManager = new DataManager(
                new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"]),
                new MapInfo(this.Configuration["Mappers:Users"]));

            Globals.Hasher = new PasswordHashService();
        }
    }
}