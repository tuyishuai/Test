using System.ComponentModel.DataAnnotations;

namespace TuYi.Practice.DTO.ValiDataExt
{
    public class PhoneValiDataAttribute : RegularExpressionAttribute
    {
        public PhoneValiDataAttribute() :base("^1[3589][0-9]{9}$")
        {
            ErrorMessage = "请输入正确的手机号码";
        }
    }
}
