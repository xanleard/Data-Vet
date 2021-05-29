// <copyright file="ApplicationDbContext.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using VET.DataBase.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
          DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        public DbSet<TypeAnimal> TypeAnimals { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
