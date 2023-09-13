using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuYi.Practice.Framework.CustomEnum
{
    /// <summary>
    /// 用户类别
    /// </summary>
    public enum UserTypeEnum
    {
        /// <summary>
        /// 后台用户
        /// </summary>
        [Remark("后台用户")]
        Administrators = 1,

        /// <summary>
        /// 普通用户
        /// </summary>
        [Remark("普通用户")]
        Member = 2,

        /// <summary>
        /// 主播用户
        /// </summary>
        [Remark("主播用户")]
        Anchor = 3
    }
}
