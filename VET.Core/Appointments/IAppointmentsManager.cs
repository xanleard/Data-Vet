// <copyright file="IAppointmentsManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.Appointments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VET.Core;
    using VET.DataBase.Models;

    public interface IAppointmentsManager
    {
        Task<OperationResult> CreateAsync(Appointment appointment);

        Task<OperationResult> EditAsync(Appointment appointment);

        Task<OperationResult> DeleteAsync(Appointment appointment);

        Task<Appointment> FindByIdAsync(int id);

        Task<IEnumerable<Appointment>> GetAllAsync();

    }
}
