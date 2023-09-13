using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuYi.Practice.DTO.ValiDataExt;
using TuYi.Practice.Framework.CustomEnum;

namespace TuYi.Practice.DTO
{
    public class UserInfoDTO
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [Display(Name = "后台用户名称")]
        public string? Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>      
        [Required(ErrorMessage = "密码不能为空")]
        [Display(Name = "密码")]
        [Compare("ConfirmPassword", ErrorMessage = "两次密码不一致")]
        public string? Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "确认密码不能为空")]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string? ConfirmPassword { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>    
        [Display(Name = "用户类型")]
        public int? UserType { get; set; }

        public string? UserTypeRemark
        {
            get
            {
                return UserType == null ? null : ((UserTypeEnum)UserType).GetRemark();
            }
        }

        /// <summary>
        /// 状态
        /// </summary>           
        public int Status { get; set; }

        public string? StatusRemark
        {
            get
            {
                return ((StatusEnum)Status).GetRemark();
            }
        }

        /// <summary>
        /// 手机号码
        /// </summary>     
        public string? Phone { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>          
        [Display(Name = "手机号")]
        [Required(ErrorMessage = "手机号不能为空")]
        [PhoneValiData]
        public string? Mobile { get; set; }

        /// <summary>
        /// 地址
        /// </summary>    
        [Display(Name = "地址")]
        public string? Address { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>           
        public string? Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>           
        public long? QQ { get; set; }

        /// <summary>
        /// 微信
        /// </summary>           
        public string? WeChat { get; set; }

        /// <summary>
        /// 性别
        /// </summary>       
        [Display(Name = "性别")]
        [Required(ErrorMessage = "性别不能为空")]
        public byte? Sex { get; set; }

        public string? SexRemark
        {
            get
            {
                return Sex == null ? null : ((GenderEnum)Sex).GetRemark();
            }
        }

        /// <summary>
        /// 最后登录时间
        /// </summary>           
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>           
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>           
        public int CreateId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>           
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人Id
        /// </summary>           
        public int? LastModifyId { get; set; }
    }
}
