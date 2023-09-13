using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuYi.Practice.Interfaces;
using System.Linq.Expressions;
using SqlSugar;
using TuYi.Practice.Framework.Models;

namespace TuYi.Practice.Services
{
    public class BaseService : IBaseService
    {
        protected ISqlSugarClient _sqlSugarClient;

        public BaseService(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }

        #region Query

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> FindAsync<T>(int id) where T : class
        {
            return _sqlSugarClient.Queryable<T>().InSingleAsync(id);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        public ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return _sqlSugarClient.Queryable<T>().Where(funcWhere);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="funcOrderby"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public async Task<PagingData<T>> QueryPageAsync<T>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, object>> funcOrderby, bool isAsc = true) where T : class
        {
            var list = _sqlSugarClient.Queryable<T>();

            if (funcWhere != null)
            {
                list = list.Where(funcWhere);
            }

            list = list.OrderByIF(true, funcOrderby, isAsc ? OrderByType.Asc : OrderByType.Desc);
            List<T> dataList = await list.ToPageListAsync(pageIndex, pageSize);

            PagingData<T> result = new PagingData<T>()
            {
                DataList = dataList,
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = list.Count(),
            };

            return result;
        }

        /// <summary>
        /// 原始查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("尽量别用")]
        public ISugarQueryable<T> Set<T>() where T : class
        {
            return _sqlSugarClient.Queryable<T>();
        }

        #endregion

        #region Add

        /// <summary>
        /// 单个新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<T> InsertAsync<T>(T t) where T : class, new()
        {
            await _sqlSugarClient.Insertable(t).ExecuteCommandAsync();

            return t;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> InsertAsync<T>(List<T> insertList) where T : class, new()
        {
            await _sqlSugarClient.Insertable(insertList).ExecuteCommandAsync();

            return insertList;
        }

        #endregion

        #region Delete

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync<T>(int id) where T : class, new()
        {
            T t = _sqlSugarClient.Queryable<T>().InSingle(id);
            await _sqlSugarClient.Deleteable(t).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync<T>(T t) where T : class, new()
        {
            await _sqlSugarClient.Deleteable(t).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <returns></returns>
        public async Task DeleteAsync<T>(List<T> tList) where T : class
        {
            await _sqlSugarClient.Deleteable(tList).ExecuteCommandAsync();
        }

        #endregion

        #region Update

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateAsync<T>(T t) where T : class, new()
        {
            if (t == null) throw new Exception("t is null");

            await _sqlSugarClient.Updateable(t).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateList"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateAsync<T>(List<T> updateList) where T : class, new()
        {
            if (updateList == null) throw new Exception("t is null");

            await _sqlSugarClient.Updateable(updateList).ExecuteCommandAsync();
        }

        #endregion

        #region Other

        /// <summary>
        /// 执行原始Sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ISugarQueryable<T> ExcuteQuery<T>(string sql) where T : class, new()
        {
            return _sqlSugarClient.SqlQueryable<T>(sql);
        }

        /// <summary>
        /// 释放上下文
        /// </summary>
        public void Dicpose()
        {
            if (_sqlSugarClient != null)
            {
                _sqlSugarClient.Dispose();
            }
        }

        #endregion

    }
}
