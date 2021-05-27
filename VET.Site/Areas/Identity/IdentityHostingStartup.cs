// <copyright file="IdentityHostingStartup.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(VET.Site.Areas.Identity.IdentityHostingStartup))]

namespace VET.Site.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}