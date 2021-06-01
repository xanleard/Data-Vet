// <copyright file="IUnitMeasurementsManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.UnitMeasurements
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VET.DataBase.Models;

    public interface IUnitMeasurementsManager
    {
        Task<UnitMeasurement> FindByIdAsync(int id);

        Task<IEnumerable<UnitMeasurement>> GetAllAsync();
    }
}
