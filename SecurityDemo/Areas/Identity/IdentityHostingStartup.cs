using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecurityDemo.Areas.Identity.Data;
using SecurityDemo.Data;

[assembly: HostingStartup(typeof(SecurityDemo.Areas.Identity.IdentityHostingStartup))]
namespace SecurityDemo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SecurityDemoContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("SecurityDemoContextConnection")));

                services.AddDefaultIdentity<SecurityDemoUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SecurityDemoContext>();
            });
        }
    }
}