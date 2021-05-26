// <copyright file="VETSeedData.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Seed
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using VET.DataBase.Contexts;
    using VET.DataBase.Models;

    public class VETSeedData
    {
        public static async Task EnsureVETSeedData(IServiceProvider serviceProvider)
        {
            bool databaseCreated = false;
            try
            {
                databaseCreated = await serviceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreatedAsync();
            }
            catch (Exception)
            {
                throw;
            }

            if (!databaseCreated)
            {
                return;
            }

            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.TypeAnimals.Add(new TypeAnimal { Id = 1, Description = "Perro" });
            context.TypeAnimals.Add(new TypeAnimal { Id = 2, Description = "Gato" });
            await context.SaveChangesAsync();
        }
    }
}
