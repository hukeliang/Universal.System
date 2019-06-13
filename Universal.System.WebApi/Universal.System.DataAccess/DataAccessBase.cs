using Universal.System.DataAccess.Interface;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Universal.System.Common.CustomAttribute;
using System;

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
        /// 操作数据库链接
        /// </summary>
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// 操作数据库链接
        /// </summary>
        IDbConnection IDataAccessBase<TEntity>.DBConnection => _dbConnection;

        static DataAccessBase()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);  
        }

        public DataAccessBase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        #region 同步方法

        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public TEntity Get<TKey>(TKey id) => _dbConnection.Get<TEntity>(id);

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList() => _dbConnection.GetList<TEntity>();

        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(object whereConditions) => _dbConnection.GetList<TEntity>(whereConditions);

        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        public IEnumerable<TEntity> GetList(string conditions, object parameters = null) => _dbConnection.GetList<TEntity>(conditions, parameters);

        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="rowsPerPage">每页显示数据</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null) => _dbConnection.GetListPaged<TEntity>(pageNumber, rowsPerPage, conditions, orderby, parameters);

        /// <summary>
        /// 插入一条记录并返回主键值(自增类型返回主键值，否则返回null)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int? Insert(TEntity entity) => _dbConnection.Insert(entity);

        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>影响的行数</returns>
        public int Update(TEntity entity) => _dbConnection.Update(entity);

        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        public int Delete<TKey>(TKey id) => _dbConnection.Delete(id);

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        public int Delete(TEntity entity) => _dbConnection.Delete(entity);

        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        public int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null) => _dbConnection.DeleteList<TEntity>(whereConditions, transaction, commandTimeout);

        /// <summary>
        /// 使用where子句删除多个记录
        /// </summary>
        /// <param name="conditions">where子句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        public int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null) => _dbConnection.DeleteList<TEntity>(conditions, parameters, transaction, commandTimeout);

        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int RecordCount(string conditions = "", object parameters = null) => _dbConnection.RecordCount<TEntity>(conditions, parameters);

        #endregion

        #region 异步

        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public Task<TEntity> GetAsync<TKey>(TKey id) => _dbConnection.GetAsync<TEntity>(id);

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> GetListAsync() => _dbConnection.GetListAsync<TEntity>();

        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> GetListAsync(object whereConditions) => _dbConnection.GetListAsync<TEntity>(whereConditions);

        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> GetListAsync(string conditions, object parameters = null) => _dbConnection.GetListAsync<TEntity>(conditions, parameters);

        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="rowsPerPage">每页显示数据</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
            => _dbConnection.GetListPagedAsync<TEntity>(pageNumber, rowsPerPage, conditions, orderby, parameters);

        /// <summary>
        /// 插入一条记录并返回主键值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<int?> InsertAsync(TEntity entity) => _dbConnection.InsertAsync(entity);

        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>影响的行数</returns>
        public Task<int> UpdateAsync(TEntity entity) => _dbConnection.UpdateAsync(entity);

        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        public Task<int> DeleteAsync<TKey>(TKey id) => _dbConnection.DeleteAsync<TEntity>(id);

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        public Task<int> DeleteAsync(TEntity entity) => _dbConnection.DeleteAsync(entity);

        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        public Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null) => _dbConnection.DeleteListAsync<TEntity>(whereConditions, transaction, commandTimeout);

        /// <summary>
        /// 使用where子句删除多个记录
        /// </summary>
        /// <param name="conditions">wher子句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        public Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null) => DeleteListAsync(conditions, parameters, transaction, commandTimeout);

        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<int> RecordCountAsync(string conditions = "", object parameters = null) => _dbConnection.RecordCountAsync<TEntity>(conditions, parameters);


        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // 要检测冗余调用
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    _dbConnection?.Close();
                    _dbConnection?.Dispose();

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
