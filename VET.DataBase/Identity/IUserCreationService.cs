// <copyright file="IUserCreationService.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Identity
{
    using System.Threading.Tasks;

    public interface IUserCreationService
    {
        Task CreateUser();
    }
}
