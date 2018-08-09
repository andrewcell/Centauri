using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Centauri.Models;

namespace Centauri
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<IronManContext>()
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            Console.WriteLine("Testing Database connection...");
            MySQL mysql = new MySQL("root", "root", "local.mac", 6974, "Centauri_dev");
            MySQL.Result rst = mysql.ConnectionTest();
            if (rst.Success)
            {
                Console.WriteLine("MySQL Connection Test Success");
               // Console.WriteLine("MySQL Server : " + rst.Data[0]);
            }
            else
            {
                Console.WriteLine("MySQL caused error : " + rst.Data[0]);
            }
            Console.WriteLine(Configuration["MySQL:Host"]);
            Models.MongoDB mongo = new Models.MongoDB("mongodb://local.mac:27017/");
            string mongoResult = mongo.TestConnection();
            if (mongoResult == "")
            {
                Console.WriteLine("MongoDB connection test passed.");
            }
            else
            {
                Console.WriteLine("MongoDB casued error : " + mongoResult);
            }
            app.UseMvc();
        }
    }
}
