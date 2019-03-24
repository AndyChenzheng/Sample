﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Czar.Cms.Core.Repository
{
    /// <summary>
    /// 基础的仓库接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseRepository<T,TKey>:IDisposable where T:class
    {
        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        T Get(TKey id);

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();

        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        IEnumerable<T> GetList(object whereConditions);

        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<T> GetList(string conditions, object parameters = null);

        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有paging的强类型list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPerpage"></param>
        /// <param name="conditions"></param>
        /// <param name="orderby"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> GetListPaged(int pageNumber, int rowsPerpage, string conditions, string orderby,
            object parameters = null);

        /// <summary>
        /// 插入一条记录并返回主键值（自增类型返回主健值，否则返回null）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int? Insert(T entity);

        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(TKey id);

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(T entity);

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
        /// <param name="conditions">where子句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null);

        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int RecordCount(string conditions = "", object parameter = null);

        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<T> GetAsync(TKey id);

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync();

        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(object whereConditions);

        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(string conditions,object parameters=null);

        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有paging的强类型list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="conditions"></param>
        /// <param name="orderby"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListPageAsync(int pageNumber, int rowsPerPage, string conditions, string orderby,
            object parameters = null);
        /// <summary>
        /// 插入一条记录并返回主键值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int?> InsertAsync(T entity);

        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TKey id);

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(T entity);

        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null,
            int? commandTimeout = null);

        /// <summary>
        /// 使用where子句删除多条记录
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null);

        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> RecordCountAsync(string conditions = "", object parameters = null);



    }
}