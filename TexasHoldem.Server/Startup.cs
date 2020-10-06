using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexasHoldem.Server.Core.Network;
using TexasHoldem.Server.Core.Services;
using TexasHoldem.Server.DAL;

namespace TexasHoldem.Server
{
    public class Startup
    { 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=TexasHoldem;Trusted_Connection=True;MultipleActiveResultSets=true",
                    x => x.MigrationsAssembly("TexasHoldem.Server.DAL.Migrations"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IUserService, UserService>();

            var serviceProvider = services.BuildServiceProvider();

            var server = new TexasHoldem.Server.Core.Network.Server(serviceProvider);
            server.Start();

            services.AddSingleton(server);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
