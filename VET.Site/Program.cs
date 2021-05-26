// <copyright file="Program.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using VET.DataBase.Seed;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var varHost = CreateWebHostBuilder(args).Build();
            using (var scope = varHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    await VETSeedData.EnsureVETSeedData(scope.ServiceProvider);
                }
                catch (Exception)
                {
                }
            }

            varHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>();
    }
}
