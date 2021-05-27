// <copyright file="CustomersManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using VET.DataBase.Models;
    using VET.DataBase.Repositories;

    public class CustomersManager : ICustomersManager
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomersManager(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public Task<OperationResult> CreateAsync(Customer customer)
        {
            if (customer == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerCreateAsync(customer);
        }

        public Task<OperationResult> DeleteAsync(Customer customer)
        {
            if (customer == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerDeleteAsync(customer);
        }

        public Task<OperationResult> EditAsync(Customer customer)
        {
            if (customer == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerEditAsync(customer);
        }

        public async Task<Customer> FindByIdAsync(int id)
        {
            return await this.customerRepository.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await this.customerRepository.All().ToListAsync();
        }

        private async Task<OperationResult> InnerEditAsync(Customer customer)
        {
            var consult = await this.customerRepository.All().AnyAsync(x => x.IdentificationCard == customer.IdentificationCard && x.Id != customer.Id);

            if (!consult)
            {
                this.customerRepository.Update(customer);
                await this.customerRepository.SaveChangesAsync();
                return new OperationResult(true);
            }

            return new OperationResult(new Dictionary<string, IEnumerable<string>> { [nameof(customer.IdentificationCard)] = new[] { "Ya existe TypeAnimal con ese nombre." } });
        }

        private async Task<OperationResult> InnerDeleteAsync(Customer customer)
        {
            this.customerRepository.Delete(customer);
            await this.customerRepository.SaveChangesAsync();
            return new OperationResult(true);
        }

        private async Task<OperationResult> InnerCreateAsync(Customer customer)
        {
            var consult = await this.customerRepository.All().AnyAsync(x => x.IdentificationCard == customer.IdentificationCard);

            if (!consult)
            {
                this.customerRepository.Create(customer);
                await this.customerRepository.SaveChangesAsync();
                return new OperationResult(true);
            }

            return new OperationResult(new Dictionary<string, IEnumerable<string>> { [nameof(customer.IdentificationCard)] = new[] { "Ya existe una Cedula." } });
        }
    }
}
