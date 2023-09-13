using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuYi.Practice.Interfaces;
using SqlSugar;

namespace TuYi.Practice.Services
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserInfoService : BaseService, IUserInfoService
    {
        public UserInfoService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }
    }
}
