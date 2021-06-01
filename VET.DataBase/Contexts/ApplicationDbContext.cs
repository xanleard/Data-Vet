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

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Sex> Sexes { get; set; }

        public DbSet<UnitMeasurement> UnitMeasurements { get; set; }

        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Animal>()
             .HasOne(c => c.TypeAnimals)
             .WithMany()
             .HasForeignKey(c => c.TypeAnimalId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Animal>()
            .HasOne(c => c.Customers)
            .WithMany()
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Animal>()
           .HasOne(c => c.Sexes)
           .WithMany()
           .HasForeignKey(c => c.SexId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Animal>()
           .HasOne(c => c.UnitMeasurements)
           .WithMany()
           .HasForeignKey(c => c.UnitMeasurementId)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
