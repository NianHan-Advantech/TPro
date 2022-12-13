using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TPro.EntityFramework.DbProvider
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 是否已提交
        /// </summary>
        bool Committed { get; }

        /// <summary>
        /// 提交事务
        /// </summary>
        int Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollBack();

        /// <summary>
        /// 创建 TEntity 类型的实体容器，用来对实体进行相关的操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        /// <summary>
        /// 执行任意 Command 命名的 SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlRaw(string sqlCommand, params object[] parameters);

        /// <summary>
        /// 将指定的实体附加到集的基础上下文中, 实体的状态更改为 Unchanged，不会被提交到数据库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        void Attach<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// 将指定的实体的状态更改为 Modified, 会被提交到数据库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        void SetModified<TEntity>(TEntity item) where TEntity : class;

        List<string> GetAllTableNames();
    }
}