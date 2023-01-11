using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TPro.EntityFramework.DbProvider;

namespace TPro.EntityFramework.Data
{
    public class Repository<T> : IRepository where T : class, new()
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 带入 Unit Of Work Name
        /// </summary>
        /// <param name="unitofworkname"></param>
        public Repository(DbAttr dbType)
        {
            _unitOfWork = new UnitOfWork(dbType);
        }

        /// <summary>
        /// 默认 Adv.Entity.{0}.Name  {0} 位置即为 Unit Of Work Name
        /// </summary>
        public Repository()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// 获取该仓储的工作单元
        /// </summary>
        internal IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        /// 将指定的聚合根添加到仓储中。
        /// </summary>
        /// <param name="entity">要添加的实体对象</param>
        public void Add(T entity)
        {
            GetSet().Add(entity);
        }

        /// <summary>
        /// 将仓储集或数据库中 entity 修改
        /// </summary>
        /// <param name="entity">要修改的实体对象</param>
        public void Modify(T entity)
        {
            _unitOfWork.SetModified(entity);
        }

        /// <summary>
        /// 从仓储集或数据库中移除 entity
        /// </summary>
        /// <param name="entity">要移除的实体对象</param>
        public void Remove(T entity)
        {
            // Attach 实例（如果 DbContext 没有跟踪实例的话）
            _unitOfWork.Attach(entity);

            // removed
            GetSet().Remove(entity);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            return _unitOfWork.Commit() > 0;
        }

        /// <summary>
        /// 新增单个实体后提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddThenCommit(T entity)
        {
            Add(entity);
            return Commit();
        }

        /// <summary>
        ///修改单个实体后提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ModifyThenCommit(T entity)
        {
            Modify(entity);
            return Commit();
        }

        /// <summary>
        /// 删除单个实体后提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool RemoveThenCommit(T entity)
        {
            Remove(entity);
            return Commit();
        }

        /// <summary>
        /// 从仓储中读取所有实体。
        /// </summary>
        /// <returns>所有的实体</returns>
        public IQueryable<T> GetAll()
        {
            return GetSet();
        }

        /// <summary>
        /// 筛选符合条件的第一个数据
        /// </summary>
        /// <param name="filter">筛选器表达式</param>
        /// <returns></returns>
        public T GetBy(Expression<Func<T, bool>> filter)
        {
            return GetSet().FirstOrDefault(filter);
        }

        /// <summary>
        /// 筛选符合条件的所有数据
        /// </summary>
        /// <param name="filter">筛选器表达式</param>
        /// <returns></returns>
        internal IQueryable<T> GetFiltered(Expression<Func<T, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        public List<T> GetEntitiesBy(Expression<Func<T, bool>> expression)
        {
            return GetSet().Where(expression).ToList();
        }

        public List<string> GetAllTableNames()
        {
            return _unitOfWork.GetAllTableNames();
        }



        private DbSet<T> GetSet()
        {
            return _unitOfWork.CreateSet<T>();
        }

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}