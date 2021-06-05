// <copyright file="BaseSiteDbContextRepositoryBase.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Repositories
{
    using VET.DataBase.Contexts;

    public class BaseSiteDbContextRepositoryBase<TEntity> : RepositoryBase<TEntity, ApplicationDbContext>
        where TEntity : class
    {
        public BaseSiteDbContextRepositoryBase(ApplicationDbContext context)
            : base(context)
        {
            this.Context = context;
        }
    }
}
