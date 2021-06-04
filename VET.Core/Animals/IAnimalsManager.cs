// <copyright file="IAnimalsManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.Animals
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VET.Core;
    using VET.DataBase.Models;

    public interface IAnimalsManager
    {
        Task<OperationResult> CreateAsync(Animal animal);

        Task<OperationResult> EditAsync(Animal animal);

        Task<OperationResult> DeleteAsync(Animal animal);

        Task<Animal> FindByIdAsync(int id);

        Task<IEnumerable<Animal>> GetAllAsync();

        Task<IEnumerable<Animal>> GetAllCustomerAsync(int id);
    }
}
