// <copyright file="AnimalsManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.Animals
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using VET.DataBase.Models;
    using VET.DataBase.Repositories;

    public class AnimalsManager : IAnimalsManager
    {
        private readonly IRepository<Animal> animalRepository;

        public AnimalsManager(IRepository<Animal> animalRepository)
        {
            this.animalRepository = animalRepository;
        }

        public Task<OperationResult> CreateAsync(Animal animal)
        {
            if (animal == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerCreateAsync(animal);
        }

        public async Task<Animal> FindByIdAsync(int id)
        {
            return await this.animalRepository.All()
                             .Include(i => i.Customers)
                             .Include(i => i.TypeAnimals)
                             .Include(i => i.UnitMeasurements)
                             .Include(i => i.Sexes)
                             .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await this.animalRepository.All()
                             .Include(i => i.Customers)
                             .Include(i => i.TypeAnimals)
                             .Include(i => i.UnitMeasurements)
                             .Include(i => i.Sexes)
                             .ToListAsync();
        }

        public Task<OperationResult> DeleteAsync(Animal animal)
        {
            if (animal == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerDeleteAsync(animal);
        }

        public Task<OperationResult> EditAsync(Animal animal)
        {
            if (animal == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerEditAsync(animal);
        }

        private async Task<OperationResult> InnerCreateAsync(Animal animal)
        {
                this.animalRepository.Create(animal);
                await this.animalRepository.SaveChangesAsync();
                return new OperationResult(true);
        }

        private async Task<OperationResult> InnerEditAsync(Animal animal)
        {
                this.animalRepository.Update(animal);
                await this.animalRepository.SaveChangesAsync();
                return new OperationResult(true);
        }

        private async Task<OperationResult> InnerDeleteAsync(Animal animal)
        {
            this.animalRepository.Delete(animal);
            await this.animalRepository.SaveChangesAsync();
            return new OperationResult(true);
        }
    }
}
