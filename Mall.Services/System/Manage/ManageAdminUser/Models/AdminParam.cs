using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{
    public class AdminParam
    {
        [Required(ErrorMessage = "密码不为空")]
        public string? LoginUserName { get; set; }

        [Required(ErrorMessage = "密码不为空")]
        public string? LoginPassword { get; set; }

        public string? NickName { get; set; }
    }

}
