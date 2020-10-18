using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JamboPay.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JamboPay
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();

            try
            {
                var scope = host.Services.CreateScope();
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                ctx.Database.EnsureCreated();
                
                
                if (!ctx.Roles.Any())
                {
                    var ambassadorRole = new IdentityRole("Ambassador");
                    var supporterRole = new IdentityRole("​Supporter");
                    
                    roleMgr.CreateAsync(ambassadorRole).GetAwaiter().GetResult();
                    roleMgr.CreateAsync(supporterRole).GetAwaiter().GetResult();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://127.0.0.1:5000");
                });
    }
}
