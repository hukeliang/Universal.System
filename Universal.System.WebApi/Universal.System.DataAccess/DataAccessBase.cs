using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity;

namespace Universal.System.DataAccess
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [DependencyRegisterGeneric(Type = RegisterType.InstancePerLifetimeScope)]
    public class DataAccessBase<TEntity> : IDataAccessBase<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public UniversalSystemDbContext Db { get; }

        /// <summary>
        /// 指定类型的实体对象集合
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;


        public DataAccessBase(UniversalSystemDbContext dbContext)
        {
            Db = dbContext;

            _dbSet = Db.Set<TEntity>();
        }



        #region 同步方法
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual bool Insert(TEntity entity)
        {
            _dbSet.Add(entity);

            return Db.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete(int id)
        {
            TEntity obj = _dbSet.Find(id);

            _dbSet.Remove(obj);

            return Db.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Deleted;

            return Db.SaveChanges() > 0;

        }
        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <param name="key">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual bool Update(TEntity entity, object key)
        {
            bool result = false;

            TEntity existing = _dbSet.Find(key);

            if (existing != null)
            {
                Db.Entry(existing).CurrentValues.SetValues(entity);

                result = Db.SaveChanges() > 0;
            }
            return result;
        }
        
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Find(int id)
        {
            return _dbSet.Find(id);
        }
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> match)
        {
            return _dbSet.Where(match).ToList();
        }
       
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="match"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public ICollection<TEntity> Find<TKey>(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, TKey>> orderBy)
        {
            return _dbSet.Where(match).OrderBy(orderBy).ToList();
        }
        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        public virtual TEntity FindSingle(Expression<Func<TEntity, bool>> match)
        {
            return _dbSet.SingleOrDefault(match);
        }

        /// <summary>
        /// 根据条件查询数据库,是否存在
        /// </summary>
        /// <returns></returns>
        public bool FindAny(Expression<Func<TEntity, bool>> match)
        {
            return _dbSet.AsNoTracking().Any(match);
        }
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>指定对象的集合</returns>
        public virtual ICollection<TEntity> FindWithPager<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderBy, int pageSize, int pageIndex)
        {
            int excludedRows = (pageIndex - 1) * pageSize;

            return _dbSet.Where(where).OrderByDescending(orderBy).Skip(excludedRows).Take(pageSize).ToList();
        }

        /// <summary>
        /// 执行原生Sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual ICollection<TEntity> SqlQuery(string sql, params string[] parameters)
        {
            IQueryable<TEntity> query = _dbSet.FromSql(sql, parameters);

            return query.ToList();
        }

        #endregion


        #region 异步


        #endregion

        #region IDisposable Support 垃圾回收

        private bool disposedValue = false; // 要检测冗余调用
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    Db?.Dispose();

                    // TODO: 如果使用Autofac等IOC容器貌似会自动释放对象
                    Console.Out.WriteLine("对象被释放");
                }
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        /// <summary>
        /// TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        /// </summary>
        //~DataAccessBase()
        //{
        //    // 析构函数用于释放 大对象
        //    // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //    Dispose(false);
        //}

        /// <summary>
        /// 添加此代码以正确实现可处置模式。
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);

            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
