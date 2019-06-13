using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Universal.System.DataAccess.Interface
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataAccessBase<TEntity> : IDisposable where TEntity : class, new()
    {
        IDbConnection DBConnection { get; }

        #region 同步

        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        TEntity Get<TKey>(TKey id);

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetList();

        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(object whereConditions);

        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(string conditions, object parameters = null);

        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="rowsPerPage">每页显示数据</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null);

        /// <summary>
        /// 插入一条记录并返回主键值(自增类型返回主键值，否则返回null)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int? Insert(TEntity entity);

        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>影响的行数</returns>
        int Update(TEntity entity);

        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        int Delete<TKey>(TKey id);

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        int Delete(TEntity entity);

        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>

        int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 使用where子句删除多个记录
        /// </summary>
        /// <param name="conditions">wher子句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int RecordCount(string conditions = "", object parameters = null);

        #endregion

        #region 异步

        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TKey>(TKey id);

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListAsync();

        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListAsync(object whereConditions);

        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListAsync(string conditions, object parameters = null);

        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="rowsPerPage">每页显示数据</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>

        Task<IEnumerable<TEntity>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null);

        /// <summary>
        /// 插入一条记录并返回主键值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        Task<int?> InsertAsync(TEntity entity);

        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>影响的行数</returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        Task<int> DeleteAsync<TKey>(TKey id);

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>

        Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 使用where子句删除多个记录
        /// </summary>
        /// <param name="conditions">wher子句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> RecordCountAsync(string conditions = "", object parameters = null);
        #endregion
    }
}
