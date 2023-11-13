using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{
    public class AdminLoginParam
    {
        [Required(ErrorMessage = "登录名不为空")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "密码不为空")]
        public string? PasswordMd5 { get; set; }
    }

}
