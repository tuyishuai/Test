using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuYi.Practice.Framework.Models
{
    /// <summary>
    /// 分页实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingData<T> where T : class
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T>? DataList { get; set; }

        /// <summary>
        /// 查询字符串
        /// </summary>
        public string? SearchString { get; set; }
    }
}
