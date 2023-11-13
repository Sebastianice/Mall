
using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{
    public class UpdateUserInfoParam
    {
        [Required(ErrorMessage = "登录名不为空")]
        public string NickName { get; set; }= null!;

        [Required(ErrorMessage = "登录名不为空")]
        public string PasswordMd5 { get; set; } = null!;
      
        public string? IntroduceSign { get; set; }
    }
}
