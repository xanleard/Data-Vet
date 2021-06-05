// <copyright file="VETSeedData.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Seed
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using VET.DataBase.Contexts;
    using VET.DataBase.Identity;
    using VET.DataBase.Models;

    public class VETSeedData
    {
        public static async Task EnsureVETSeedData(IServiceProvider serviceProvider)
        {
            bool databaseCreated = false;
            try
            {
                databaseCreated = await serviceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreatedAsync();
                var userService = serviceProvider.GetRequiredService<IUserCreationService>();
                userService.CreateUser().GetAwaiter().GetResult();
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

            context.TypeAnimals.Add(new TypeAnimal { Description = "Perro" });
            context.TypeAnimals.Add(new TypeAnimal { Description = "Gato" });

            context.Sexes.Add(new Sex { Description = "Macho" });
            context.Sexes.Add(new Sex { Description = "Hembra" });

            context.UnitMeasurements.Add(new UnitMeasurement { Description = "lb" });
            context.UnitMeasurements.Add(new UnitMeasurement { Description = "kg" });
            await context.SaveChangesAsync();
        }
    }
}
