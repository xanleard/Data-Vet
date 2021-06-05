// <copyright file="Startup.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site
{
    using System;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using VET.Core.Animals;
    using VET.Core.Appointments;
    using VET.Core.Customers;
    using VET.Core.Sexes;
    using VET.Core.TypeAnimals;
    using VET.Core.UnitMeasurements;
    using VET.DataBase.Contexts;
    using VET.DataBase.Identity;
    using VET.DataBase.Models;
    using VET.DataBase.Repositories;

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
            services.AddControllersWithViews();
            services.AddCloudscribeNavigation(Configuration.GetSection("NavigationOptions"));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("VET.Database.Migrations.SqlServer.Migrations")).EnableSensitiveDataLogging(true).EnableDetailedErrors(true);
                options.EnableSensitiveDataLogging(true);
                options.EnableDetailedErrors(true);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserCreationService, UserCreationService>();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "ITILSICookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.Configure<PasswordHasherOptions>(option =>
            {
                option.IterationCount = 12000;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireSee", policy => policy.RequireRole("See", "Admin"));
                options.AddPolicy("RequireEmployeeSee", policy => policy.RequireRole("UserEmployee"));
            });

            services.AddScoped<IRepository<TypeAnimal>, BaseSiteDbContextRepositoryBase<TypeAnimal>>();
            services.AddScoped<IRepository<Customer>, BaseSiteDbContextRepositoryBase<Customer>>();
            services.AddScoped<IRepository<Sex>, BaseSiteDbContextRepositoryBase<Sex>>();
            services.AddScoped<IRepository<UnitMeasurement>, BaseSiteDbContextRepositoryBase<UnitMeasurement>>();
            services.AddScoped<IRepository<Animal>, BaseSiteDbContextRepositoryBase<Animal>>();
            services.AddScoped<IRepository<Appointment>, BaseSiteDbContextRepositoryBase<Appointment>>();
            services.AddScoped<ITypeAnimalsManager, TypeAnimalsManager>();
            services.AddScoped<ICustomersManager, CustomersManager>();
            services.AddScoped<ISexesManager, SexesManager>();
            services.AddScoped<IUnitMeasurementsManager, UnitMeasurementsManager>();
            services.AddScoped<IAnimalsManager, AnimalsManager>();
            services.AddScoped<IAppointmentsManager, AppointmentsManager>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
