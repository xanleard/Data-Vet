// <copyright file="ISexesManager.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core.Sexes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VET.DataBase.Models;

    public interface ISexesManager
    {
        Task<Sex> FindByIdAsync(int id);

        Task<IEnumerable<Sex>> GetAllAsync();
    }
}
