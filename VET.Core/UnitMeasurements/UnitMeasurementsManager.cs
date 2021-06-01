// <copyright file="UnitMeasurementsManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.UnitMeasurements
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using VET.DataBase.Models;
    using VET.DataBase.Repositories;

    public class UnitMeasurementsManager : IUnitMeasurementsManager
    {
        private readonly IRepository<UnitMeasurement> unitMeasurementsRepository;

        public UnitMeasurementsManager(IRepository<UnitMeasurement> unitMeasurementsRepository)
        {
            this.unitMeasurementsRepository = unitMeasurementsRepository;
        }

        public async Task<UnitMeasurement> FindByIdAsync(int id)
        {
            return await this.unitMeasurementsRepository.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<UnitMeasurement>> GetAllAsync()
        {
            return await this.unitMeasurementsRepository.All().ToListAsync();
        }
    }
}
