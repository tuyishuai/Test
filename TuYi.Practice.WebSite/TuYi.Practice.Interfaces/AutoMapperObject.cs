using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TuYi.Practice.DbModels;
using TuYi.Practice.DTO;
using TuYi.Practice.Framework.Models;

namespace TuYi.Practice.Interfaces
{
    /// <summary>
    /// 自动映射实体
    /// </summary>
    public class AutoMapperObject : Profile
    {
        public AutoMapperObject() 
        {
            CreateMap<UserInfo, UserInfoDTO>();
            CreateMap<UserInfoDTO, UserInfo>();

            CreateMap<PagingData<UserInfo>, PagingData<UserInfoDTO>>();
        }
    }
}
