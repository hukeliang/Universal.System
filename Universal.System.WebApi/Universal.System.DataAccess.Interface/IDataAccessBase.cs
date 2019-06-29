using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Universal.System.Entity;

namespace Universal.System.DataAccess.Interface
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataAccessBase<TEntity> : IDisposable where TEntity : class, new()
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        UniversalSystemDbContext Db { get; }

        #region 同步

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        bool Insert(TEntity entity);
        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool Delete(int id);
        /// <summary>
        /// 从数据库中删除指定对象
        /// </summary>
        /// <param name="entity">指定对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool Delete(TEntity entity);
        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="entity">指定的对象</param>
        /// <param name="key">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        bool Update(TEntity entity, object key);

        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">ID主键的值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        TEntity Find(int id);
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns></returns>
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> match);
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        ICollection<TEntity> Find<TKey>(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, TKey>> orderBy);
        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        TEntity FindSingle(Expression<Func<TEntity, bool>> match);
        /// <summary>
        /// 根据条件查询数据库,是否存在
        /// </summary>
        /// <returns></returns>
        bool FindAny(Expression<Func<TEntity, bool>> match);
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>指定对象的集合</returns>
        ICollection<TEntity> FindWithPager<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderBy, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 执行原生Sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ICollection<TEntity> SqlQuery(string sql, params string[] parameters);
        #endregion

        #region 异步

        #endregion

    }
}
