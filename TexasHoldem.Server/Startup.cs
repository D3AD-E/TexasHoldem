using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexasHoldemServer.Core.Network;
using TexasHoldemServer.Core.Services;
using TexasHoldemServer.DAL;

namespace TexasHoldemServer
{
    public class Startup
    { 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=TexasHoldem;Trusted_Connection=True;MultipleActiveResultSets=true",
                    x => x.MigrationsAssembly("TexasHoldemServer.DAL.Migrations"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IUserService, UserService>();

            var serviceProvider = services.BuildServiceProvider();

            var server = new Server(serviceProvider);
            server.Start();

            services.AddSingleton(server);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
