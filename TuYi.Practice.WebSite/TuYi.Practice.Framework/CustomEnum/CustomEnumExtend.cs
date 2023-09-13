using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TuYi.Practice.Framework.CustomEnum
{
    public static class CustomEnumExtend
    {
        /// <summary>
        /// 获取枚举特性备注内容
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? GetRemark(this Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());
            if (field != null && field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute? remarkAttribute = field.GetCustomAttribute<RemarkAttribute>();
                return remarkAttribute?.GetRemark();
            }
            return String.Empty;
        }

        /// <summary>
        /// 枚举转下拉列表方法
        /// </summary>
        /// <param name="customerEnum"></param>
        /// <param name="selected"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<SelectListItem> ToSelectListByEnum(Type enumType, string selected = "", string text = "请选择", string value = "")
        {
            IList<SelectListItem> listItem = new List<SelectListItem>();

            if (!string.IsNullOrWhiteSpace(text))
            {
                listItem.Add(new SelectListItem { Text = text, Value = value });
            }

            foreach (var item in Enum.GetNames(enumType))
            {
                FieldInfo? field = enumType.GetField(item);
                string remark = string.Empty;

                if (field != null && field.IsDefined(typeof(RemarkAttribute), true))
                {
                    object[] arr = field.GetCustomAttributes(typeof(RemarkAttribute), true);
                    remark = arr != null && arr.Length > 0 ? ((RemarkAttribute)arr[0]).GetRemark() : item;

                    SelectListItem selectListItem = new SelectListItem()
                    {
                        Value = ((int)Enum.Parse(enumType, item)).ToString(),
                        Text = remark,
                        Selected = false
                    };

                    if (selectListItem.Value.Equals(selected))
                    {
                        selectListItem.Selected = true;
                    }

                    listItem.Add(selectListItem);
                }
            }

            return listItem;
        }
    }

    /// <summary>
    /// 备注特性
    /// </summary>
    public class RemarkAttribute : Attribute
    {
        private string _Rmark;
        public RemarkAttribute(string remark)
        {
            this._Rmark = remark;
        }

        public string GetRemark() => _Rmark;
    }
}
