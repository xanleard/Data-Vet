// <copyright file="IRepository.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        TEntity Create();

        TEntity Create(TEntity entity);

        IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities);

        TEntity Delete(TEntity entity);

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        TEntity Find(params object[] keys);

        ValueTask<TEntity> FindAsync(params object[] keys);

        ValueTask<TEntity> FindAsync(CancellationToken token, params object[] keys);

        TEntity First(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> predicate);

        TEntity Update(TEntity entity);
    }
}
