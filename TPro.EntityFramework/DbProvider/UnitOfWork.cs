﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TPro.EntityFramework.DbContexts;

namespace TPro.EntityFramework.DbProvider
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork()
        {
            _context = new MyDbContext();
        }

        public UnitOfWork(DbAttr dbType)
        {
            _context = new MyDbContext(dbType);
        }

        public UnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }
        public bool Committed { get; private set; }

        public int Commit()
        {
            if (this.Committed)
                throw new InvalidOperationException("The unit of work has committed.");
            var result = _context.SaveChanges();
            this.Committed = true;
            return result;
        }

        public void RollBack()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(entry =>
            {
                entry.State = EntityState.Unchanged;
            });
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry<TEntity>(item).State = EntityState.Unchanged;
        }

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry<TEntity>(item).State = EntityState.Modified;
        }

        /// <summary>
        /// 获取所有表名
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllTableNames()
        {
            return _context.Model.GetEntityTypes()
                .Select(t => t.GetTableName())
                .Distinct()
                .ToList();
        }

        public List<object> GetBySql(Type entitytype, string sql)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            using (var command = connection.CreateCommand())
            {
                var list = new List<object>();
                command.CommandText = sql;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            var entity = entitytype.Assembly.CreateInstance(entitytype.FullName);
                            var properties = entitytype.GetProperties();
                            foreach (var item in properties)
                            {
                                try
                                {
                                    var value = reader[item.Name];
                                    if (item.PropertyType.Name.Contains("String"))
                                        value = value.ToString();
                                    item.SetValue(entity, value);
                                }
                                catch (Exception ex)
                                {
                                    if (ex is ArgumentException)
                                        continue;
                                    else if (ex is ArgumentOutOfRangeException)
                                        continue;
                                    throw;
                                }
                            }
                            list.Add(entity);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                return list;
            }
        }

        public int ExecuteSqlRaw(string sqlCommand, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(sqlCommand, parameters);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            //return base.Database<TEntiy>(sqlQuery, parameters);
            //return FromSqlRaw(sqlQuery, parameters);
            return null;
        }
    }
}