// <copyright file="SexesManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.Sexes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using VET.DataBase.Models;
    using VET.DataBase.Repositories;

    public class SexesManager : ISexesManager
    {
        private readonly IRepository<Sex> sexesRepository;

        public SexesManager(IRepository<Sex> sexesRepository)
        {
            this.sexesRepository = sexesRepository;
        }

        public async Task<Sex> FindByIdAsync(int id)
        {
            return await this.sexesRepository.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Sex>> GetAllAsync()
        {
            return await this.sexesRepository.All().ToListAsync();
        }
    }
}
