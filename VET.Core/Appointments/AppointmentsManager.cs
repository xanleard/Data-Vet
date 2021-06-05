// <copyright file="AppointmentsManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.Appointments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using VET.DataBase.Models;
    using VET.DataBase.Repositories;

    public class AppointmentsManager : IAppointmentsManager
    {
        private readonly IRepository<Appointment> appointmentRepository;

        public AppointmentsManager(IRepository<Appointment> appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        public Task<OperationResult> CreateAsync(Appointment appointment)
        {
            if (appointment == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerCreateAsync(appointment);
        }

        public async Task<Appointment> FindByIdAsync(int id)
        {
            return await this.appointmentRepository.All()
                             .Include(i => i.Customers)
                             .Include(i => i.Animals)
                             .ThenInclude(i => i.TypeAnimals)
                             .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await this.appointmentRepository.All()
                             .Include(i => i.Customers)
                             .Include(i => i.Animals)
                             .ThenInclude(i => i.TypeAnimals)
                             .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllCustomerAsync(int id)
        {
            return await this.appointmentRepository.All().Where(c => c.CustomerId == id)
                             .Include(i => i.Customers)
                             .Include(i => i.Animals)
                             .ThenInclude(i => i.TypeAnimals)
                             .ToListAsync();
        }

        public Task<OperationResult> DeleteAsync(Appointment appointment)
        {
            if (appointment == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerDeleteAsync(appointment);
        }

        public Task<OperationResult> EditAsync(Appointment appointment)
        {
            if (appointment == null)
            {
                return Task.FromResult(new OperationResult(false));
            }

            return this.InnerEditAsync(appointment);
        }

        private async Task<OperationResult> InnerCreateAsync(Appointment appointment)
        {
                this.appointmentRepository.Create(appointment);
                await this.appointmentRepository.SaveChangesAsync();
                return new OperationResult(true);
        }

        private async Task<OperationResult> InnerEditAsync(Appointment appointment)
        {
                this.appointmentRepository.Update(appointment);
                await this.appointmentRepository.SaveChangesAsync();
                return new OperationResult(true);
        }

        private async Task<OperationResult> InnerDeleteAsync(Appointment appointment)
        {
            this.appointmentRepository.Delete(appointment);
            await this.appointmentRepository.SaveChangesAsync();
            return new OperationResult(true);
        }
    }
}
