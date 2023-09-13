using SqlSugar;
using System.Linq.Expressions;
using TuYi.Practice.Framework.Models;

namespace TuYi.Practice.Interfaces
{
    public interface IBaseService
    {
        #region Query

        /// <summary>
        /// 根据ID查询实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindAsync<T>(int id) where T : class;

        /// <summary>
        /// 提供对表单的查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("尽量避免使用，using 带表达式目录树的 代替")]
        ISugarQueryable<T> Set<T>() where T : class;

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class;

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
        Task<PagingData<T>> QueryPageAsync<T>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, object>> funcOrderby, bool isAsc = true) where T : class;

        #endregion

        #region Add

        /// <summary>
        /// 新增数据，即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<T> InsertAsync<T>(T t) where T : class, new();

        /// <summary>
        /// 批量新增，即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertList"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> InsertAsync<T>(List<T> insertList) where T : class, new();

        #endregion

        #region Update

        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        Task UpdateAsync<T>(T t) where T : class, new();

        /// <summary>
        /// 批量更新数据，即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateList"></param>
        Task UpdateAsync<T>(List<T> updateList) where T : class, new();

        #endregion

        #region Delete

        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        Task DeleteAsync<T>(int id) where T : class, new();

        /// <su+mary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        Task DeleteAsync<T>(T t) where T : class, new();

        /// <summary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <param name="tList"></param>
        Task DeleteAsync<T>(List<T> tList) where T : class;

        #endregion

        #region Other

        /// <summary>
        /// 执行sql 返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        ISugarQueryable<T> ExcuteQuery<T>(string sql) where T : class, new();

        #endregion
    }
}
