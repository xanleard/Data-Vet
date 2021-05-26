// <copyright file="TypeAnimalsManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.TypeAnimals
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using VET.DataBase.Models;
    using VET.DataBase.Repositories;

    public class TypeAnimalsManager : ITypeAnimalsManager
    {
        private readonly IRepository<TypeAnimal> typeAnimalRepository;

        public TypeAnimalsManager(IRepository<TypeAnimal> typeAnimalRepository)
        {
            this.typeAnimalRepository = typeAnimalRepository;
        }

        public Task<OperationResult> CreateAsync(TypeAnimal typeAnimal)
        {
            if (typeAnimal == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerCreateAsync(typeAnimal);
        }

        public Task<OperationResult> DeleteAsync(TypeAnimal typeAnimal)
        {
            if (typeAnimal == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerDeleteAsync(typeAnimal);
        }

        public Task<OperationResult> EditAsync(TypeAnimal typeAnimal)
        {
            if (typeAnimal == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerEditAsync(typeAnimal);
        }

        private async Task<OperationResult> InnerCreateAsync(TypeAnimal typeAnimal)
        {
            var consult = await this.typeAnimalRepository.All().AnyAsync(x => x.Description == typeAnimal.Description);

            if (!consult)
            {
                this.typeAnimalRepository.Create(typeAnimal);
                await this.typeAnimalRepository.SaveChangesAsync();
                return new OperationResult(true);
            }

            return new OperationResult(new Dictionary<string, IEnumerable<string>> { [nameof(typeAnimal.Description)] = new[] { "Ya existe TypeAnimal con ese nombre." } });
        }

        private async Task<OperationResult> InnerEditAsync(TypeAnimal typeAnimal)
        {
            var consult = await this.typeAnimalRepository.All().AnyAsync(x => x.Description == typeAnimal.Description && x.Id != typeAnimal.Id);

            if (!consult)
            {
                this.typeAnimalRepository.Update(typeAnimal);
                await this.typeAnimalRepository.SaveChangesAsync();
                return new OperationResult(true);
            }

            return new OperationResult(new Dictionary<string, IEnumerable<string>> { [nameof(typeAnimal.Description)] = new[] { "Ya existe TypeAnimal con ese nombre." } });
        }

        private async Task<OperationResult> InnerDeleteAsync(TypeAnimal typeAnimal)
        {
            this.typeAnimalRepository.Delete(typeAnimal);
            await this.typeAnimalRepository.SaveChangesAsync();
            return new OperationResult(true);
        }

        public async Task<TypeAnimal> FindByIdAsync(int id)
        {
            return await this.typeAnimalRepository.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<TypeAnimal>> GetAllAsync()
        {
            return await this.typeAnimalRepository.All().ToListAsync();
        }

    }
}
