using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Server_API.DB;
using Server_API.Utility;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            DBProcedure.InitJsonMapper();

            string logRootPath = (string)configuration.GetValue(typeof(string), "LogRoot");
            if (!string.IsNullOrEmpty(logRootPath))
            {
                Console.WriteLine($"log root : {logRootPath}");
                Console.WriteLine("StartUp Start");
            }
            try
            {
                ConfigurationManager.Instance.Initialize(Configuration, MySqlClientFactory.Instance);
                DBTimeWatch.Instance.Initialize(ConfigurationManager.Instance.DBConfig);
                RedisProcedure.Initialize(ConfigurationManager.Instance.RedisConnectionString);

                if (!string.IsNullOrEmpty(logRootPath))
                {
                    Console.WriteLine("StartUp Done");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
