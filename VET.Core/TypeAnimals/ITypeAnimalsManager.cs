// <copyright file="ITypeAnimalsManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.TypeAnimals
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VET.Core;
    using VET.DataBase.Models;

    public interface ITypeAnimalsManager
    {
        Task<OperationResult> CreateAsync(TypeAnimal typeAnimal);

        Task<OperationResult> EditAsync(TypeAnimal typeAnimal);

        Task<OperationResult> DeleteAsync(TypeAnimal typeAnimal);

        Task<TypeAnimal> FindByIdAsync(int id);

        Task<IEnumerable<TypeAnimal>> GetAllAsync();
    }
}
