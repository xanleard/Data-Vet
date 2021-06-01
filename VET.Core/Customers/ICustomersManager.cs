// <copyright file="ICustomersManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.Customers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VET.Core;
    using VET.DataBase.Models;

    public interface ICustomersManager
    {
        Task<OperationResult> CreateAsync(Customer customer);

        Task<OperationResult> EditAsync(Customer customer);

        Task<OperationResult> DeleteAsync(Customer customer);

        Task<Customer> FindByIdAsync(int id);

        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
